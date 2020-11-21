using UnityEngine;
using Com.Technitaur.GreenBean.Helpers;
using TMPro;

namespace Com.Technitaur.GreenBean
{
    public class Controller : MonoBehaviour
    {
        public TMP_Text stateStatus;
        private InputHandler input;
        private Environment env;
        private int walkSpeed = 2;
        private int fallSpeed = 4;
        private int beltSpeedModifier = 1;
        private int ladderClimbSpeed = 1;
        private int ropeClimbUpSpeed = 1;
        private int ropeClimbDownSpeed = 2;

        private Vector3Int moveDirection;
        private bool wantsJump;
        private States state;
        private JumpData jumpData;

        public enum States
        {
            Idle,
            Walking,
            Sliding,
            JumpingUp,
            JumpingDown,
            ClimbingRope,
            ClimbingLadder,
            Falling,
            Dead
        }

        public void Start()
        {
            input = GetComponent<InputHandler>();
            env = GetComponent<Environment>();
            state = States.Idle;
        }
        
        public void Update()
        {
            InputUpdate();
            stateStatus.text = "State: " + state;
        }


        public bool CanJump()
        {
            switch (state)
            {
                case States.Walking:
                case States.Idle:
                case States.ClimbingRope:
                    return true;
                default:
                    return false;
            }
        }

        public void InputUpdate()
        {
            if (input.canGetValues)
            {
                moveDirection = input.Direction;
                wantsJump = input.desiredJump;
                input.desiredJump = false;
                input.canGetValues = false;
            }

            if (!CanJump()) wantsJump = false;
        }

        public bool DoJumpingUp()
        {
            if (state != States.JumpingUp) return false;
            if (jumpData.hasPeaked)
            {
                state = States.JumpingDown;
                return false;
            }
            else
            {
                jumpData.NextStep();
                IncrementalMove(jumpData.xdir, jumpData.ydir, jumpData.xdist, jumpData.ydist);
                return true;
            }
        }

        public bool DoJumpingDown()
        {
            if (state != States.JumpingDown) return false;
            jumpData.NextStep();
            IncrementalMove(jumpData.xdir, jumpData.ydir, jumpData.xdist, Mathf.Abs(jumpData.ydist));
            if (env.IsGrounded)
            {
                jumpData = null;
                state = States.Idle;
            }
            return true;
        }

        public bool DoClimbingLadder()
        {
            if (state != States.ClimbingLadder) return false;
            if (moveDirection.y != 0)
            {
                // TODO: movement code
                if (env.IsGrounded)
                {
                    state = States.Idle;
                    return true;
                }
            }
            return true;
        }

        public bool DoClimbingRope()
        {
            if (state != States.ClimbingRope) return false;
            SetLastGroundedPos();
            if (moveDirection.y < 0)
            {
                IncrementalMove(0, -1, 0, 2);
                return true;
            }
            if (moveDirection.y > 0)
            {
                IncrementalMove(0, 1, 0, 1);
                return true;
            }
            return false;
        }

        public bool DoSliding()
        {
            if (state != States.Sliding) return false;
            SetLastGroundedPos();
            return false;
        }

        public bool DoFalling()
        {
            if (state != States.Falling) return false;
            if (env.IsGrounded)
            {
                state = States.Idle;
                return true;
            }
            IncrementalMove(0, -1, 0, 4);
            return false;
        }

        public bool StartJump()
        {
            if (!wantsJump) return false;
            wantsJump = false;
            jumpData = new JumpData(moveDirection.x);
            state = States.JumpingUp;
            if (DoJumpingUp()) return true;
            return false;
        }

        public bool StartClimbingLadder()
        {
            bool climbUp = moveDirection.y > 0 && env.IsAtLadder;
            bool climbDown = moveDirection.y < 0 && env.IsAboveLadder;
            if (!climbUp && !climbDown) return false;

            state = States.ClimbingLadder;
            IncrementalMove(0, moveDirection.y, 0, ladderClimbSpeed);
            return true;
        }

        public bool StartClimbingRopeStanding()
        {
            bool climbDown = moveDirection.y < 0 && env.IsAboveRopeTop;
            if (!climbDown) return false;
            state = States.ClimbingRope;
            IncrementalMove(0, -1, 0, ropeClimbDownSpeed);
            return true;
        }

        public bool DoWalking()
        {
            if (moveDirection.x == 0)
            {
                Debug.Log("Setting idle state.");
                state = States.Idle;
                return false;
            }
            if (StartJump()) return true;
            if (StartClimbingLadder()) return true;
            if (StartClimbingRopeStanding()) return true;
            if (moveDirection.x > 0)
            {
                if (env.RightBlocked) return false;
                int speed = walkSpeed;
                if (env.IsOnLeftBelt) speed--;
                if (env.IsOnRightBelt) speed++;
                IncrementalMove(1, 0, speed, 0);
            }
            if (moveDirection.x < 0)
            {
                if (env.LeftBlocked) return false;
                int speed = walkSpeed;
                if (env.IsOnLeftBelt) speed++;
                if (env.IsOnRightBelt) speed--;
                IncrementalMove(-1, 0, speed, 0);
            }
            if (!env.IsGrounded) state = States.Falling;
            return true;
        }

        public bool IdleConveyed()
        {
            if (env.IsOnLeftBelt)
            {
                transform.position += Vector3Int.left;
            }
            if (env.IsOnRightBelt)
            {
                transform.position += Vector3Int.right;
            }
            if (!env.IsGrounded) state = States.Falling;
            return true;
        }

        public bool DoIdle()
        {
            if (state != States.Idle) return false;
            if (!env.IsGrounded)
            {
                state = States.Falling;
                return true;
            }

            if (moveDirection.x != 0)
            {
                Debug.Log("Set walking state.");
                state = States.Walking;
                return true;
            }
            if (StartJump()) return true;
            if (StartClimbingLadder()) return true;
            if (StartClimbingRopeStanding()) return true;
            if (IdleConveyed()) return true;
            return true;
        }

        public void FixedUpdate()
        {
            env.EnvUpdate();

            int xdir = moveDirection.x;
            int ydir = moveDirection.y;

            if (!env.IsGrounded)
            {
                if (DoJumpingUp()) return;
                if (DoJumpingDown()) return;
                if (DoClimbingLadder()) return;
                if (DoClimbingRope()) return;
                if (DoSliding()) return;
                if (DoFalling()) return;
            }
            else
            {
                SetLastGroundedPos();
                if (DoWalking()) return;
                if (DoClimbingLadder()) return;
            }
            DoIdle();
        }

        public Vector3Int GetJumpDestination()
        {
            return Vector3Int.zero;
        }

        public bool CheckGroundedChange(bool current)
        {
            if (env.IsGrounded != current)
                return true;
            else
                return false;
        }

        public bool TryMoveHorizontal(int dir)
        {
            if (dir == 0) return false;

            if (dir < 0 && !env.LeftBlocked)
            {
                MoveLeft();
                return true;
            }

            if (dir > 0 && !env.RightBlocked)
            {
                MoveRight();
                return true;
            }
            return false;
        }

        public bool TryMoveVertical(int dir)
        {
            if (dir == 0) return false;

            bool conditionsMet = false;
            if (dir > 0)
            {
                if (env.IsAtLadder) conditionsMet = true;
                switch (state)
                {
                    case States.ClimbingLadder:
                    case States.ClimbingRope:
                    case States.JumpingUp:
                        {
                            conditionsMet = true;
                            break;
                        }
                    default: break;
                }

                if (!conditionsMet) return false;

                MoveUp();
                return true;
            }
            else if (dir < 0)
            {
                if (env.IsAboveLadder || env.IsAboveRopeTop) conditionsMet = true;
                switch (state)
                {
                    case States.ClimbingLadder:
                    case States.ClimbingRope:
                    case States.Falling:
                    case States.JumpingDown:
                    case States.Sliding:
                        {
                            conditionsMet = true;
                            break;
                        }
                    default: break;
                }

                if (!conditionsMet) return false;

                MoveDown();
                return true;
            }
            return false;
        }

        public void MoveUp() => transform.position += Vector3Int.up;
        public void MoveRight() => transform.position += Vector3Int.right;
        public void MoveLeft() => transform.position += Vector3Int.left;
        public void MoveDown() => transform.position += Vector3Int.down;

        public void IncrementalMove(int xdir, int ydir, int xunits, int yunits)
        {
            SetLastFramePos();
            bool startingGroundedState = env.IsGrounded;
            while (xunits > 0 || yunits > 0)
            {
                if (xunits > 0)
                {
                    TryMoveHorizontal(xdir);
                    if (CheckGroundedChange(startingGroundedState)) break;
                    xunits--;
                }
                if (yunits > 0)
                {
                    TryMoveVertical(ydir);
                    if (CheckGroundedChange(startingGroundedState)) break;
                    yunits--;
                }
            }
            if (!startingGroundedState && env.IsGrounded)
            {
                state = States.Idle;
                transform.position = Vector3Int.RoundToInt(transform.position);
            }
        }

        public void SetLastFramePos()
        {
            env.lastFramePos = env.pos;
        }

        public void SetLastGroundedPos()
        {
            env.lastGroundedPosition = env.pos;
        }

        public void Fall()
        {
            transform.position = transform.position - Vector3Int.down * fallSpeed;
        }

    }
}