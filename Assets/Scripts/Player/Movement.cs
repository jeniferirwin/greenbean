using UnityEngine;
using UnityEngine.InputSystem;
using GreenBean.Helpers;
using GreenBean.InputHandling;

namespace GreenBean.Player
{
    public class Movement : MonoBehaviour
    {
        [Header("Setup")]
        public Rigidbody2D rb;
        public InputHandler inputHandler;
        public EnvironmentCheck envCheck;
        [Header("Climbing")]
        public bool canClimbLadder;
        public bool canClimbRope;

        private JumpData jumpData;
        private Vector2 moveDirection;
        private Vector2 initPosition;

        private Vector2 walkVector = new Vector2(2f / 8f, 0f);
        private Vector2 fallVector = new Vector2(0f, -4f / 8f);
        private Vector2 falseVector = new Vector2(-32500f, 32500f);

        private void Start()
        {
            jumpData = null;
            rb.gravityScale = 0;
            initPosition = transform.position;
        }

        public void Reset(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                transform.position = initPosition;
                jumpData = null;
                rb.gravityScale = 0;
            }
        }

        private void FixedUpdate()
        {
            if (envCheck.Grounded && jumpData == null)
            {
                ProcessGroundState();
            }
            else
            {
                ProcessAirState();
            }

            envCheck.LastPosition = transform.position;
        }


        private void ProcessGroundState()
        {
            if (inputHandler.canGetValues)
            {
                inputHandler.canGetValues = false;
                moveDirection = inputHandler.desiredDirection;

                if (inputHandler.desiredJump)
                {
                    inputHandler.desiredJump = false;
                    InitiateJump(moveDirection);
                    return;
                }
            }

            if (moveDirection != Vector2.zero)
            {
                Vector2 change = moveDirection * 2 / 8f;
                change = SetBlocks(change);
                Vector2 newPosition = (Vector2)transform.position + change;
                transform.position = newPosition;
            }
        }

        private void GroundSnap()
        {
            float nearestUp = Mathf.Ceil(transform.position.y);
            transform.position = new Vector2(transform.position.x, nearestUp);
        }

        private Vector2 SetBlocks(Vector2 change)
        {
            float wantX = change.x;
            if (wantX > 0 && envCheck.RightBlocked || wantX < 0 && envCheck.LeftBlocked)
            {
                Debug.Log("Setting block...");
                change.x = 0;
            }
            return change;
        }

        private void ProcessAirState()
        {
            if (jumpData != null)
            {
                MoveJumpStep(jumpData);
            }
            else
            {
                Vector2 dest = (Vector2)transform.position + (Vector2.down * 4 / 8f);
                Vector2 collPoint = envCheck.CollisionIntersect(transform.position, dest);
                if (collPoint == falseVector)
                {
                    transform.position = dest;
                }
                else
                {
                    transform.position = collPoint;
                }
            }
        }

        private void InitiateJump(Vector2 dir)
        {
            jumpData = new JumpData(dir.x);
            MoveJumpStep(jumpData);
        }

        private void MoveJumpStep(JumpData data)
        {
            Vector2 change = data.GetNextChange();
            change = SetBlocks(change);
            Vector2 dest = (Vector2)transform.position + change;
            Debug.Log(dest);

            if (!jumpData.hasPeaked)
            {
                transform.position = dest;
                return;
            }

            Vector2 collPoint = envCheck.CollisionIntersect(transform.position, dest);
            if (collPoint == falseVector)
            {
                transform.position = dest;
            }
            else
            {
                transform.position = collPoint;
                jumpData = null;
            }
        }
    }
}