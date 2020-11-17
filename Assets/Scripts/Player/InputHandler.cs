using UnityEngine;
using UnityEngine.InputSystem;

namespace GreenBean.InputHandling
{
    public class InputHandler : MonoBehaviour
    {
        public int bufferLength;
        
        public Vector2 desiredDirection;
        public bool desiredJump;
        
        private int moveWait;
        private int jumpWait;
        
        public bool canGetValues;

        public void OnMovement(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                Vector2 input = context.ReadValue<Vector2>();
                desiredDirection = SanitizeInput(input);
            }
            else if (context.canceled)
            {
                desiredDirection = Vector2.zero;
            }
            moveWait = bufferLength;
            canGetValues = false;
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                desiredJump = true;
            }
            jumpWait = bufferLength;
            canGetValues = false;
        }

        private void Start()
        {
            desiredDirection = Vector2.zero;
            desiredJump = false;
            canGetValues = false;
        }

        private void FixedUpdate()
        {
            if (canGetValues)
                return;

            if (jumpWait > 0 || moveWait > 0)
            {
                jumpWait--;
                moveWait--;
                return;
            }
            else
            {
                canGetValues = true;
            }
        }
        
        public Vector2 SanitizeInput(Vector2 input)
        {
            if (input.x < 0)
            {
                return Vector2.left;
            }
            else if (input.x > 0)
            {
                return Vector2.right;
            }
            else if (input.y > 0)
            {
                return Vector2.up;
            }
            else if (input.y < 0)
            {
                return Vector2.down;
            }
            else
            {
                return Vector2.zero;
            }
        }
    }
}