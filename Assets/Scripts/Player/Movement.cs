using UnityEngine;
using UnityEngine.InputSystem;
using GreenBean.Helpers;
using GreenBean.EnvironmentData;
using GreenBean.InputHandling;

namespace GreenBean.Player
{
    public class Movement : MonoBehaviour
    {
        [Header("Setup")]
        public Rigidbody2D rb;
        public InputHandler inputHandler;
        [Header("LayerMasks")]
        public LayerMask whatIsRopes;
        public LayerMask whatIsDoors;
        public LayerMask whatIsLadders;
        public LayerMask whatIsCollectibles;
        public LayerMask whatIsGround;
        public LayerMask whatIsBelts;
        [Header("Walking")]
        public WallChecker leftWallChecker;
        public WallChecker rightWallChecker;
        public GroundChecker groundChecker;
        public LadderChecker ladderChecker;
        public RopeChecker ropeChecker;
        public float moveSpeed;
        [Header("Jumping")]
        public float maxFallDistance;
        [Header("Climbing")]
        public float ladderClimbSpeed;
        public float ropeClimbSpeed;
        public bool canClimbLadder;
        public bool canClimbRope;

        private JumpData jumpData;
        private Vector2 moveDirection;
        private float residualJumpXDirection;
        private Vector2 initPosition;

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
            ProcessAirState();
            ProcessGroundState();
        }

        private Vector2 WillCollide(Vector2 dest)
        {
            RaycastHit2D hit;
            Vector2 difference = dest - (Vector2) transform.position;
            hit = Physics2D.Raycast(transform.position, difference.normalized, difference.magnitude, whatIsGround + whatIsBelts);
            if (hit.rigidbody != null)
            {
                Debug.Log(hit.point);
                return hit.point;
            }
            else
            {
                return new Vector2(-32500, 32500);
            }
        }

        private void ProcessGroundState()
        {
            if (!Physics2D.Raycast(transform.position,Vector2.down,0.05f,whatIsGround + whatIsBelts))
            {
                return;
            }
            else
            {
                GroundSnap();
            }
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
                Vector2 newPosition = (Vector2) transform.position + (moveDirection * 2 / 8f);
                if (rightWallChecker.blocked && moveDirection.x > 0)
                {
                    newPosition.x = transform.position.x;
                }
                if (leftWallChecker.blocked && moveDirection.x < 0)
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

        private void ProcessClimbingState()
        {

        }

        private void ProcessAirState()
        {
            Debug.Log("Processing air state...");
            if (Physics2D.Raycast(transform.position,Vector2.down,0.05f,whatIsGround + whatIsBelts))
            {
                Debug.Log("We are grounded.");
                GroundSnap();
                return;
            }
            if (jumpData != null)
            {
                MoveJumpStep(jumpData);
            }
            else
            {
            Vector2 dest = (Vector2) transform.position + (Vector2.down * 4 / 8f);
            Vector2 collPoint = WillCollide(dest);
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
            if (change.x > 0 && rightWallChecker.blocked)
            {
                change.x = 0;
            }
            if (change.x < 0 && leftWallChecker.blocked)
            {
                change.x = 0;
            }
            Vector2 dest = (Vector2) transform.position + change;
            Vector2 collPoint = WillCollide(dest);
            if (collPoint.x == -32500 && collPoint.y == 32500)
                transform.position = (Vector2) transform.position + change;
            else
                transform.position = collPoint;
        }
    }
}