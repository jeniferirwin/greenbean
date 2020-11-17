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
        [Header("Walking")]
        public WallChecker leftWallChecker;
        public WallChecker rightWallChecker;
        public LayerMask whatIsGround;
        public GroundChecker groundChecker;
        public LadderChecker ladderChecker;
        public RopeChecker ropeChecker;
        public float moveSpeed;
        [Header("Jumping")]
        public float maxFallDistance;
        [Header("Climbing")]
        public float climbSpeed;

        private JumpData jumpData;
        private Vector2 moveDirection;
        private float residualJumpXDirection;
        private Vector2 initPosition;

        private void Start()
        {
            jumpData = null;
            rb.gravityScale = 1;
            initPosition = transform.position;
        }
        
        public void Reset(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                transform.position = initPosition;
                jumpData = null;
                rb.gravityScale = 1;
            }
        }

        private void FixedUpdate()
        {
            moveDirection = inputHandler.queuedDirection;
            if (jumpData != null)
            {
                ProcessAirState();
                return;
            }

            if (groundChecker.isGrounded)
            {
                ProcessGroundState();
            }
            else
            {
                ProcessAirState();
            }
        }

        private void ProcessGroundState()
        {
            if (jumpData == null && inputHandler.jumpQueued)
            {
                InitiateJump();
                return;
            }

            if (residualJumpXDirection != 0)
                residualJumpXDirection = 0;

            if (Mathf.Abs(moveDirection.x) == 1f)
            {
                if (moveDirection.x > 0 && rightWallChecker.blocked)
                    return;

                if (moveDirection.x < 0 && leftWallChecker.blocked)
                    return;

                transform.Translate(moveDirection * moveSpeed * Time.fixedDeltaTime);
                return;
            }

            transform.Translate(Vector2.zero);
        }

        private void ProcessClimbingState()
        {

        }

        private void ProcessAirState()
        {
            if (jumpData != null)
            {
                if (jumpData.counter >= jumpData.movementPerFixedUpdate.Length)
                {
                    residualJumpXDirection = jumpData.xval;
                    jumpData = null;
                    return;
                }
                rb.gravityScale = 0;
                Vector2 movement = jumpData.movementPerFixedUpdate[jumpData.counter];
                if (movement.x > 0 && rightWallChecker.blocked)
                {
                    movement.x = 0;
                }
                if (movement.x < 0 && leftWallChecker.blocked)
                {
                    movement.x = 0;
                }
                if (Physics2D.Raycast(transform.position, Vector2.down, 0.25f, whatIsGround) && jumpData.counter > 5)
                {
                    jumpData = null;
                    return;
                }
                transform.Translate(movement);
                jumpData.counter++;
                if (groundChecker.isGrounded && jumpData.counter > 3)
                {
                    jumpData = null;
                    rb.gravityScale = 1;
                }
            }
            else if (residualJumpXDirection != 0 && !groundChecker.isGrounded)
            {
                Vector2 fallDirection = new Vector2(residualJumpXDirection, Physics2D.gravity.y);
                if (Physics2D.Raycast(transform.position, Vector2.down, 0.25f, whatIsGround))
                {
                    residualJumpXDirection = 0;
                    return;
                }
                transform.Translate(fallDirection * Time.fixedDeltaTime);
            }
            else
            {
                rb.gravityScale = 1;
                rb.velocity = Physics2D.gravity;
            }
        }

        private void InitiateJump()
        {
            jumpData = new JumpData(gameObject, moveDirection.x);
            inputHandler.jumpQueued = false;
        }
    }
}