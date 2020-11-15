using UnityEngine;

namespace GreenBean.Helpers
{
    public class InputData
    {
        public float jumpQueueLength;
        public bool wantsJump;
        
        private float wantsJumpTimer;
        
        public InputData(float queueLength)
        {
            jumpQueueLength = queueLength;
        }

        public Vector2 GetMoveDirection(Vector2 input)
        {
            /*
             * Because of the sensitivity of the gamepad I'm using, I'm
             * noticing that I have a lot of problems getting sucked onto
             * ladders when I don't want to in the original version of the
             * game in VICE. I decided that I'm going to just make the code
             * prefer left-right movement over up-down movement - if the
             * horizontal axis is getting any input, we'll ignore the
             * vertical axis.
             * 
             * It's kind of a hack, but it should make for fewer accidents
             * on fiddly controllers.
            */

            if (input.x < 0)
            {
                return Vector2.left;
            }
            if (input.x > 0)
            {
                return Vector2.right;
            }
            if (input.y > 0)
            {
                return Vector2.up;
            }
            if (input.y < 0)
            {
                return Vector2.down;
            }
            return Vector2.zero;
        }
        
        public void Update()
        {
            if (wantsJumpTimer > 0f)
            {
                wantsJumpTimer -= Time.deltaTime;
            }
            else if (wantsJumpTimer <= 0f && wantsJump)
            {
                wantsJump = false;
            }
        }
        
        public void QueueJump()
        {
            wantsJumpTimer = jumpQueueLength; 
            wantsJump = true;
        }
        
        public void UnQueueJump()
        {
            wantsJumpTimer = 0f;
            wantsJump = false;
        }
    }
}