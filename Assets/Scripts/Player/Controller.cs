﻿using UnityEngine;
using Com.Technitaur.GreenBean.Helpers;

namespace Com.Technitaur.GreenBean
{
    public class Controller : MonoBehaviour
    {
        private InputHandler input;
        private Environment env;
        private int ppfWalk = 2;
        private int ppfFall = 4;
        private int ppfBelt = 1;
        private int ppfClimbLadder = 1;
        private bool frameProcessed;

        private Vector2 moveDirection;
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
            frameProcessed = false;
            input = GetComponent<InputHandler>();
            env = GetComponent<Environment>();
            state = States.Idle;
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

        public void FixedUpdate()
        {
            env.EnvUpdate();
            
            if (env.IsGrounded && Mathf.Abs(env.pos.y) % 1 > 0)
            {
                transform.position = env.SnapToFloor(env.pos);
                return;
            }

            if (input.canGetValues)
            {
                moveDirection = input.Direction;
                wantsJump = input.desiredJump;
                input.desiredJump = false;
                input.canGetValues = false;
            }
            Vector2 horizontal = new Vector2(moveDirection.x, 0);
            Vector2 vertical = new Vector2(0, moveDirection.y);

            if (!CanJump())
            {
                wantsJump = false;
            }
            
            if (!env.IsGrounded)
            {
                if (state == States.JumpingUp)
                {
                    if (jumpData.hasPeaked)
                    {
                        state = States.JumpingDown;
                    }
                    else
                    {
                        transform.position = GetJumpDestination();
                        return;
                    }
                }
                if (state == States.JumpingDown)
                {
                    transform.position = GetJumpDestination();
                    if (env.IsGrounded)
                    {
                        jumpData = null;
                        state = States.Idle;
                        transform.position = env.SnapToFloor(env.pos);
                        return;
                    }
                    return;
                }
                if (state == States.ClimbingLadder)
                {
                    if (moveDirection.y != 0)
                    {
                        Vector2 climbDir = new Vector2(0,moveDirection.y);
                        PixelMove(climbDir,ppfClimbLadder);
                        if (env.IsGrounded)
                        {
                            state = States.Idle;
                            return;
                        }
                    }
                }
                if (state == States.ClimbingRope)
                {
                    // TODO
                }
                if (state == States.Sliding)
                {
                    // TODO
                }
                if (state == States.Falling)
                {
                    PixelMove(Vector2.down, ppfFall);
                    if (env.IsGrounded)
                    {
                        state = States.Idle;
                    }
                }
            }
            else
            {
                SetLastGroundedPos();
                if (wantsJump)
                {
                    wantsJump = false;
                    state = States.JumpingUp;
                    jumpData = new JumpData(moveDirection.x);
                    transform.position = GetJumpDestination();
                    return;
                }
                if (moveDirection.y > 0 && env.IsAtLadder)
                {
                    state = States.ClimbingLadder;
                    PixelMove(Vector2.up, ppfClimbLadder);
                    return;
                }
                else if (moveDirection.y < 0 && env.IsAboveLadder)
                {
                    state = States.ClimbingLadder;
                    PixelMove(Vector2.down, ppfClimbLadder);
                    return;
                }
                bool canMoveRight = moveDirection.x > 0 && !env.RightBlocked;
                bool canMoveLeft = moveDirection.x < 0 && !env.LeftBlocked;
                if (canMoveRight || canMoveLeft)
                {
                    int speed = ppfWalk;
                    if (env.IsOnLeftBelt && canMoveRight) speed--;
                    if (env.IsOnLeftBelt && canMoveLeft) speed++;
                    if (env.IsOnRightBelt && canMoveRight) speed++;
                    if (env.IsOnRightBelt && canMoveLeft) speed--;
                    PixelMove(horizontal, speed);

                    if (!env.IsGrounded)
                    {
                        state = States.Falling;
                    }
                    return;
                }
                if (env.IsOnLeftBelt)
                {
                    PixelMove(new Vector2(-1,0),1);
                    if (!env.IsGrounded) state = States.Falling;
                    return;
                }
                if (env.IsOnRightBelt)
                {
                    PixelMove(new Vector2(1,0),1);
                    if (!env.IsGrounded) state = States.Falling;
                    return;
                }
            }
        }

        public Vector2 GetJumpDestination()
        {
            float addX = jumpData.XPPF * env.pixel * jumpData.xdir;
            float addY = jumpData.YPPF * env.pixel;
            Vector2 newPos = env.pos;
            newPos += new Vector2(addX, addY);
            if (addX > 0 && env.RightBlocked)
                newPos.x = env.pos.x;
            if (addX < 0 && env.LeftBlocked)
                newPos.x = env.pos.x;

            newPos = env.Pixelize(newPos);
            return newPos;
        }

        public void PixelMove(Vector2 direction, int speed)
        {
            SetLastFramePos();
            float distance = speed * env.pixel;
            transform.position = env.Pixelize(env.pos + direction * speed * env.pixel);
        }

        public void SetLastFramePos()
        {
            env.lastFramePos = env.pos;
        }

        public void SetLastGroundedPos()
        {
            env.lastGroundedPosition = env.pos;
        }

    }
}