using UnityEngine;
using UnityEngine.InputSystem;
using GreenBean.Helpers;
using GreenBean.EnvironmentData;

namespace GreenBean.Player
{
    public class Movement : MonoBehaviour
    {
        [Header("Setup")]
        public Rigidbody2D rb;
        [Header("Walking")]
        public WallChecker leftWallChecker;
        public WallChecker rightWallChecker;
        public LayerMask whatIsGround;
        public GroundChecker groundChecker;
        public float moveSpeed;
        [Header("Jumping")]
        public float jumpForce;
        public float jumpHeight;
        public float jumpQueueLength;
        public float maxFallDistance;
        public float defaultGravity;
        public float droppingGravity;
        [Header("Climbing")]
        public float climbSpeed;

        private InputData inputData;
        private JumpData jumpData;
        private Vector2 moveDirection;

        private void Start()
        {
            inputData = new InputData(jumpQueueLength);
        }

        public void OnMovement(InputAction.CallbackContext context)
        {
            Vector2 value = context.ReadValue<Vector2>();
            moveDirection = inputData.GetMoveDirection(value);
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                inputData.QueueJump();
            }
            else if (context.canceled)
            {
                inputData.UnQueueJump();
            }
        }

        private void Update()
        {
            inputData.Update();

            if (jumpData != null)
            {
                jumpData.Update();
            }

            if (groundChecker.isGrounded)
            {
                rb.gravityScale = defaultGravity;
                ProcessGroundState();
            }
            else
            {
                ProcessAirState();
            }
        }

        private void ProcessGroundState()
        {
            if (jumpData == null && inputData.wantsJump)
            {
                InitiateJump();
                inputData.UnQueueJump();
                return;
            }

            if (jumpData != null && jumpData.leaveGroundTimer <= 0f && groundChecker.isGrounded)
            {
                jumpData = null;
            }
            
            rb.velocity = moveDirection;
        }

        private void ProcessClimbingState()
        {

        }

        private void ProcessAirState()
        {
            if (jumpData != null)
            {
                float absVel = Mathf.Abs(rb.velocity.x);
                float minVel = 0.1f;
                if (jumpData.peaked)
                {
                    rb.gravityScale = droppingGravity;
                }
                if (jumpData.direction.x > 0f && absVel < minVel)
                {
                    rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
                }
                else if (jumpData.direction.x < 0f && absVel < minVel)
                {
                    rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
                }
            }
            else
            {
                if (rb.velocity.x != 0)
                {
                    rb.velocity = new Vector2(0, rb.velocity.y);
                }
                if (rb.velocity.y > -9.81f)
                {
                    rb.velocity = new Vector2(0, -9.81f);
                }
                rb.gravityScale = droppingGravity;
            }
        }

        private void InitiateJump()
        {
            Vector2 direction = moveDirection;

            jumpData = new JumpData(gameObject, jumpHeight, direction);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}