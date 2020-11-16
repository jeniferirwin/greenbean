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
        public LadderChecker ladderChecker;
        public RopeChecker ropeChecker;
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
        private bool wantsJump;

        private void Start()
        {
            inputData = new InputData(jumpQueueLength);
            jumpData = null;
            rb.gravityScale = 0;
        }

        public void OnMovement(InputAction.CallbackContext context)
        {
            Vector2 value = context.ReadValue<Vector2>();
            inputData.GetAxis(value);
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                inputData.hasJump = true;
            }
            else if (context.canceled)
            {
                inputData.hasJump = false;
            }
        }

        private void Update()
        {
            inputData.Update();
            if (inputData.cycleFinished)
            {
                moveDirection = inputData.currentAxis;
                inputData.currentAxis = Vector2.zero;

                if (inputData.hasJump)
                {
                    inputData.hasJump = false;
                    wantsJump = true;
                }

                inputData.cycleFinished = false;
            }
        }

        private void FixedUpdate()
        {
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
            if (jumpData == null && wantsJump)
            {
                Debug.Log("Trying to jump.");
                InitiateJump();
                wantsJump = false;
                return;
            }

            if (jumpData != null && jumpData.counter > 3)
            {
                Debug.Log("Canceling jump.");
                jumpData = null;
            }

            if (Mathf.Abs(moveDirection.x) == 1f)
            {
                rb.velocity = moveDirection * moveSpeed;
            }
            else
            {
                rb.velocity = Vector2.zero;
            }
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
                    jumpData = null;
                    return;
                }
                Vector2 movement = jumpData.movementPerFixedUpdate[jumpData.counter];
                transform.Translate(movement);
                jumpData.counter++;
            }
            else
            {
                rb.velocity = Physics2D.gravity;
            }
        }

        private void InitiateJump()
        {
            Debug.Log("Jump initiated.");
            jumpData = new JumpData(gameObject, moveDirection.x);
        }
    }
}