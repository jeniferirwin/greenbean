using UnityEngine;
using UnityEngine.InputSystem;
using Com.Technitaur.GreenBean.Core;

namespace Com.Technitaur.GreenBean.Input
{
    public class InputHandler : MonoBehaviour
    {
        public int frameBufferLength;
        public Vector2Int dir;
        public bool jump;
        public bool start;
        public bool inIntro;
        public bool isClimbingInIntro;

        private bool bufferedJump;
        public Vector2Int bufferedDir;
        private bool bufferedStart;

        private bool hLock;
        private bool vLock;

        private int currentFrameBuffer;

        public struct InputData
        {
            public Vector2Int dir;
            public bool jump;
            public bool start;
        }

        public void IntroState(bool isInIntro, bool climbing)
        {
            inIntro = isInIntro;
            isClimbingInIntro = climbing;
            if (isInIntro == false && isClimbingInIntro == false)
            {
                bufferedDir = new Vector2Int(0,0);
            }
        }

        public InputData GetData()
        {
            InputData newData = new InputData();
            if (inIntro)
            {
                if (isClimbingInIntro)
                {
                    dir = new Vector2Int(1, -1);
                }
                else
                {
                    dir = new Vector2Int(0, 0);
                }
            }
            newData.dir = dir;
            newData.jump = jump;
            newData.start = start;
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
                start = bufferedStart;
            }
            else
            {
                currentFrameBuffer--;
            }
        }

        public void OnMovement(InputAction.CallbackContext context)
        {
            if (GameStatus.gameIsOver) return;
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
            if (GameStatus.gameIsOver) GameStatus.ReloadGame();
            if (context.started) vLock = true;
            if (context.canceled) vLock = false;
        }

        public void OnHorizontalLock(InputAction.CallbackContext context)
        {
            if (context.started) hLock = true;
            if (context.canceled) hLock = false;
        }

        public void OnStart(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                bufferedStart = true;
            }
            else if (context.canceled)
            {
                bufferedStart = false;
            }
        }
        public void OnJump(InputAction.CallbackContext context)
        {
            if (inIntro)
            {
                bufferedJump = false;
                return;
            }

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