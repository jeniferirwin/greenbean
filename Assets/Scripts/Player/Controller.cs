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

        private Vector2 moveDirection;
        private bool wantsJump;

        private bool isIdle;
        private bool isWalking;
        private bool isSliding;
        private bool isJumpingUp;
        private bool isJumpingDown;
        private bool isClimbingRope;
        private bool isClimbingLadder;
        private bool isFalling;
        private bool isDead;

        public void Start()
        {
            frameProcessed = false;
            input = GetComponent<InputHandler>();
            env = GetComponent<Environment>();
        }

        public void FixedUpdate()
        {
            frameProcessed = false;

            if (input.canGetValues)
            {
                moveDirection = input.Direction;
                wantsJump = input.desiredJump;
                input.canGetValues = false;
            }
            
            if (env.IsGrounded) return;

            return;

            if (!env.IsGrounded)
            {
                if (!isClimbingRope)
                {
                    wantsJump = false;
                }
            }
            else
            {
                bool canMoveRight = moveDirection.x != 0 && !env.RightBlocked;
                bool canMoveLeft = moveDirection.x != 0 && !env.LeftBlocked;
                if (canMoveRight || canMoveLeft)
                {
                    
                }
            }
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