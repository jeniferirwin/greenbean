using UnityEngine;

namespace Com.Technitaur.GreenBean
{
    public class Controller : MonoBehaviour
    {
        private InputHandler input;
        private Environment env;
        private int ppfWalk = 2;
        private int ppfFall = 4;
        private int ppfBelt = 1;
        private bool frameProcessed;

        public void Start()
        {
            frameProcessed = false;
            input = GetComponent<InputHandler>();
            env = GetComponent<Environment>();
        }

        public void FixedUpdate()
        {
            frameProcessed = false;
            ProcessWalking();
            if (!env.IsGrounded)
            {
                transform.position = env.Pixelize(env.pos + Vector2.down * ppfFall * env.pixel);
            }
            else
            {
            }
            env.lastFramePos = env.pos;
        }
        
        public void ProcessStanding()
        {
            
        }

        public void ProcessWalking()
        {
            if (!env.IsGrounded) return;

            
            if (frameProcessed) return;
            bool rightMove = input.Direction.x > 0 && !env.RightBlocked;
            bool leftMove = input.Direction.x < 0 && !env.LeftBlocked;

            if (leftMove || rightMove)
            {
                transform.position = env.SnapToFloor(env.Pixelize(env.pos + input.Direction * ppfWalk * env.pixel));
                frameProcessed = true;
            }
        }
        
        public void ProcessBeltWalking()
        {
            frameProcessed = true;
        }

        public void ProcessFallingState()
        {
            if (frameProcessed) return;
        }
    }
}