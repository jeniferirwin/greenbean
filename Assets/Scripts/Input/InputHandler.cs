using UnityEngine;
using UnityEngine.InputSystem;

namespace Com.Technitaur.GreenBean.Input
{
    public class InputHandler : MonoBehaviour
    {
        public int bufferLength;
        
        public Vector3Int Direction;
        public bool desiredJump;
        
        private int moveWait;
        private int jumpWait;
        
        public bool canGetValues;

        public void OnMovement(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                Vector2 input = context.ReadValue<Vector2>();
                Direction = SanitizeInput(input);
            }
            else if (context.canceled)
            {
                Direction = Vector3Int.zero;
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
            Direction = Vector3Int.zero;
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
        
        public Vector3Int SanitizeInput(Vector2 input)
        {
            if (input.x < 0)
            {
                return Vector3Int.left;
            }
            else if (input.x > 0)
            {
                return Vector3Int.right;
            }
            else if (input.y > 0)
            {
                return Vector3Int.up;
            }
            else if (input.y < 0)
            {
                return Vector3Int.down;
            }
            else
            {
                return Vector3Int.zero;
            }
        }
    }
}