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
            // TODO: ProcessAirState();
            ProcessGroundState();
        }


        private void ProcessGroundState()
        {
            if (inputHandler.canGetValues)
            {
                inputHandler.canGetValues = false;
                moveDirection = inputHandler.desiredDirection;
                transform.position = (Vector2) transform.position + (moveDirection * 2 / 8f);
                RaycastHit2D hitInfo;
                hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, envCheck.whatIsGround + envCheck.whatIsBelts);
                if (hitInfo.rigidbody != null)
                {
                    Debug.Log(hitInfo.point);
                    Debug.Log(hitInfo.collider);
                    Debug.Log(hitInfo.collider.bounds);
                }
                //  TODO:
                /*
                if (inputHandler.desiredJump)
                {
                    inputHandler.desiredJump = false;
                    InitiateJump(moveDirection);
                    return;
                }
                */
            }
            return;
            if (moveDirection != Vector2.zero)
            {
                Vector2 newPosition = (Vector2) transform.position + (moveDirection * 2 / 8f);
                if (moveDirection.x > 0) // TODO: RIGHT WALL CHECKER
                {
                    newPosition.x = transform.position.x;
                }
                if (moveDirection.x < 0) // TODO: LEFT WALL CHECKER
                {
                    newPosition.x = transform.position.x;
                }
                transform.position = newPosition;
            }
        }
        
        private void GroundSnap()
        {
            float nearestUp = Mathf.Ceil(transform.position.y);
            transform.position = new Vector2(transform.position.x, nearestUp);
        }

        private void ProcessAirState()
        {
            if (jumpData != null)
            {
                MoveJumpStep(jumpData);
            }
            else
            {
            Vector2 dest = (Vector2) transform.position + (Vector2.down * 4 / 8f);
            Vector2 collPoint = envCheck.CollisionIntersect(transform.position, dest);
            Debug.Log(collPoint);
            if (collPoint.x == -32500 && collPoint.y == 32500)
            {
                Debug.Log("From " + transform.position + " to " + dest);
                transform.position = dest;
            }
            else
                transform.position = collPoint;
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
            if (change.x > 0) // TODO: LEFT WALL CHECKER
            {
                change.x = 0;
            }
            if (change.x < 0) // TODO: RIGHT WALL CHECKER
            {
                change.x = 0;
            }
            Vector2 dest = (Vector2) transform.position + change;
            Vector2 collPoint = envCheck.CollisionIntersect(transform.position, dest);
            if (collPoint.x == -32500 && collPoint.y == 32500)
                transform.position = (Vector2) transform.position + change;
            else
                transform.position = collPoint;
        }
        
        private void TryMove(Vector2 change)
        {
            Vector2 dest = (Vector2) transform.position + change;
            Vector2 collPoint = envCheck.CollisionIntersect(transform.position, dest);
        }
        
        private void Fall()
        {
        }
    }
}