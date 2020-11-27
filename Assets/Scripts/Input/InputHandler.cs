using UnityEngine;
using UnityEngine.InputSystem;

namespace Com.Technitaur.GreenBean.Input
{
    public class InputHandler : MonoBehaviour
    {
        public int bufferLength;

        private InputData bufData;
        private Buffer buf;

        public struct InputData
        {
            public Vector2Int dir;
            public bool jump;
            public bool hLock;
            public bool vLock;
        }

        public struct Buffer
        {
            public int frames;

            public Buffer(int length)
            {
                frames = length;
            }
        }
        
        public void Start()
        {
            buf = new Buffer();
            buf.frames = 0;
        }

        public void FixedUpdate()
        {
            if (buf.frames > 0) buf.frames--;
        }

        public void StartBuffer()
        {
            if (buf.frames == 0)
                buf = new Buffer(bufferLength);
        }
        
        public bool HasData()
        {
            if (buf.frames > 0) return false;
            return true;
        }

        public InputData GetData()
        {
            if (bufData.hLock) bufData.dir.y = 0;
            if (bufData.vLock) bufData.dir.x = 0;
            InputData copy = bufData;
            bufData = new InputData();
            bufData.dir = copy.dir;
            return copy;
        }

        public void OnMovement(InputAction.CallbackContext context)
        {
            StartBuffer();
            Vector2 input = context.ReadValue<Vector2>();
            Debug.Log(input);
            int x = (int)Mathf.Ceil(input.x);
            int y = (int)Mathf.Ceil(input.y);
            Vector2Int rounded = new Vector2Int(x, y);
            bufData.dir = rounded;
        }

        public void OnVerticalLock(InputAction.CallbackContext context)
        {
            StartBuffer();
            if (context.started)
            {
                bufData.vLock = true;
            }
            if (context.canceled)
            {
                bufData.vLock = false;
            }
        }

        public void OnHorizontalLock(InputAction.CallbackContext context)
        {
            StartBuffer();
            if (context.started)
            {
                bufData.hLock = true;
            }
            if (context.canceled)
            {
                bufData.hLock = false;
            }
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            StartBuffer();
            if (context.started)
            {
                bufData.jump = true;
            }
        }
    }
}