using UnityEngine;

namespace GreenBean.Helpers
{
    public class KeyData
    {
        public bool left;
        public bool right;
        public bool up;
        public bool down;
        public bool jump;

        public void UpdateDpad(Vector2 input)
        {
            ResetDpadInput();
            if (input.x < 0)
                left = true;
            if (input.x > 0)
                right = true;
            if (input.y > 0)
                up = true;
            if (input.y < 0)
                down = true;
        }
        
        public void UpdateJump(float value)
        {
            ResetJumpInput();
            if (value > 0)
                jump = true;
        }
        
        public void ResetJumpInput()
        {
            jump = false;
        }
        
        public void ResetDpadInput()
        {
            left = false;
            right = false;
            up = false;
            down = false;
        }
    }
}