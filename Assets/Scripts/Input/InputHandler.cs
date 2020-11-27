using UnityEngine;
using UnityEngine.InputSystem;

namespace Com.Technitaur.GreenBean.Input
{
    public class InputHandler : MonoBehaviour
    {
        public int frameBufferLength;
        public Vector2Int dir;
        public bool jump;

        private bool bufferedJump;
        private Vector2Int bufferedDir;

        private bool hLock;
        private bool vLock;
        
        private int currentFrameBuffer;
        
        public struct InputData
        {
            public Vector2Int dir;
            public bool jump;
        }
        
        public InputData GetData()
        {
            InputData newData = new InputData();
            newData.dir = dir;
            newData.jump = jump;
            return newData;
        }

        private void Start()
        {
            hLock = false;
            vLock = false;
            currentFrameBuffer = frameBufferLength;
        }
        
        private void FixedUpdate()
        {
            if (currentFrameBuffer <= 0)
            {
                currentFrameBuffer = frameBufferLength;
                dir = bufferedDir;
                jump = bufferedJump;
            }
            else
            {
                currentFrameBuffer--;
            }
        }
        
        public void OnMovement(InputAction.CallbackContext context)
        {
            Vector2 input = context.ReadValue<Vector2>();
            int x = (int)Mathf.Ceil(input.x);
            int y = (int)Mathf.Ceil(input.y);
            Vector2Int rounded = new Vector2Int(x, y);
            if (vLock) rounded.x = 0;
            if (hLock) rounded.y = 0;
            bufferedDir = rounded;
        }

        public void OnVerticalLock(InputAction.CallbackContext context)
        {
            if (context.started) vLock = true;
            if (context.canceled) vLock = false;
        }

        public void OnHorizontalLock(InputAction.CallbackContext context)
        {
            if (context.started) hLock = true;
            if (context.canceled) hLock = false;
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                bufferedJump = true;
            }
            else if (context.canceled)
            {
                bufferedJump = false;
            }
        }
    }
}