using UnityEngine;
using UnityEngine.InputSystem;
using GreenBean.Helpers;

namespace GreenBean.Player
{
    public class Movement : MonoBehaviour
    {
        public Rigidbody2D rb;
        public BoxCollider2D groundCollider;
        public BoxCollider2D wallCollider;
        public LayerMask whatIsGround;
        public float wallCastDistance;
        public float groundCastDistance;
        public float jumpForce;
        public float jumpHeight;
        public float moveSpeed;
        public float fallSpeed;
        public float defaultGravity;
        public float droppingGravity;

        private KeyData keyData;
        private JumpData jumpData;
        private EnvData envData; 
        private Vector2 moveDirection;

        private void Start()
        {
            envData = new EnvData(gameObject, groundCollider, wallCollider, wallCastDistance, groundCastDistance, whatIsGround);
            keyData = new KeyData();
        }

        public void Walk(InputAction.CallbackContext context)
        {
            moveDirection = context.ReadValue<Vector2>();
            keyData.UpdateDpad(moveDirection);
        }
        
        public void Jump(InputAction.CallbackContext context)
        {
            keyData.UpdateJump(context.ReadValue<float>());
        }
        
        private void Update()
        {
            envData.Update();
            if (jumpData != null)
                jumpData.Update();

            if (envData.grounded)
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
            if (jumpData == null && keyData.jump)
            {
                InitiateJump();
                return;
            }

            if (jumpData != null && jumpData.leaveGroundTimer > 0f)
                return;
            else if (jumpData != null && jumpData.leaveGroundTimer <= 0f)
                jumpData = null;
                
            if (keyData.left && !envData.leftBlocked)
                rb.velocity = Vector2.left * moveSpeed;
            else if (keyData.right && !envData.rightBlocked)
                rb.velocity = Vector2.right * moveSpeed;
            else
                rb.velocity = new Vector2(0,rb.velocity.y);
            
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
                if (jumpData.direction.x > 0f && absVel < minVel && !envData.rightBlocked)
                {
                    rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
                }
                else if (jumpData.direction.x < 0f && absVel < minVel && !envData.leftBlocked)
                {
                    rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
                }
            }
            else
            {
                if (rb.velocity.x != 0)
                {
                    rb.velocity = new Vector2(0,rb.velocity.y);
                }
                if (rb.velocity.y > -9.81f)
                {
                    rb.velocity = new Vector2(0,-9.81f);
                }
                rb.gravityScale = droppingGravity;
            }
        }
        
        private void InitiateJump()
        {
            Vector2 direction = Vector2.zero;
            if (keyData.right)
                direction = Vector2.right;
            if (keyData.left)
                direction = Vector2.left;

            jumpData = new JumpData(gameObject, jumpHeight, direction);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}