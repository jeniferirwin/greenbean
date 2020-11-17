using UnityEngine;
using UnityEngine.InputSystem;

namespace GreenBean.InputHandling
{
    public class InputHandler : MonoBehaviour
    {
        public float bufferLength;
        public float currentBuffer;

        public Vector2 bufferedDirection;
        public bool bufferedJump;
        
        public bool jumpQueued;
        public Vector2 queuedDirection;

        public bool leftPressed;
        public bool rightPressed;
        public bool upPressed;
        public bool downPressed;
        public bool jumpPressed;

        public void OnMovement(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                Vector2 input = context.ReadValue<Vector2>();
                bufferedDirection = SanitizeInput(input);
            }
            else if (context.canceled)
            {
                bufferedDirection = Vector2.zero;
            }
            StartBuffer();
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                bufferedJump = true;
                StartBuffer();
            }
        }

        private void StartBuffer()
        {
            if (currentBuffer > 0)
            {
                return;
            }
            else
            {
                queuedDirection = bufferedDirection;
                currentBuffer = bufferLength;
            }
        }

        private void Start()
        {
            currentBuffer = bufferLength;
            bufferedDirection = Vector2.zero;
            jumpQueued = false;
            bufferedJump = false;
            bufferedDirection = Vector2.zero;
        }

        private void FixedUpdate()
        {
            if (currentBuffer > 0)
            {
                currentBuffer--;
            }
            else
            {
                if (bufferedJump)
                {
                    jumpQueued = true;
                    bufferedJump = false;
                }
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