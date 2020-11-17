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

        private void ProcessGroundState()
        {
            if (!groundChecker.isGrounded)
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
                transform.position = (Vector2) transform.position + (moveDirection / 8f);
            }
        }
        
        private void GroundSnap()
        {
            transform.position = new Vector2(transform.position.x, groundChecker.yPoint);
        }

        private void ProcessClimbingState()
        {

        }

        private void ProcessAirState()
        {
            Debug.Log("Processing air state...");
            if (groundChecker.isGrounded)
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
                transform.position = (Vector2) transform.position + (Vector2.down / 8f);
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
            transform.position = (Vector2)transform.position + change;
        }
    }
}