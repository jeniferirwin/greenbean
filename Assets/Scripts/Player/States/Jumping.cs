﻿using UnityEngine;
using Com.Technitaur.GreenBean.Input;
using Com.Technitaur.GreenBean.Core;

namespace Com.Technitaur.GreenBean.Player
{
    public class Jumping : PlayerBaseState
    {
        public int currentStep;
        public Vector2Int sideways;
        public Vector2Int vertical;
        public int xdir = 0;
        public bool groundAware;
        public Vector2Int startPos;
        public bool tooFar;

        public int[] JumpInfo = new int[]
        {
            3, 3,
            2, 2, 2, 2,
            1, 1, 1, 1, 1,
            0, 0, 0, 0,
            -1, -1, -1, -1, -1,
            -2, -2, -2, -2,
            -4
        };

        // do the sideways movement first, then the vertical movement
        public override void EnterState(Controller controller, InputHandler.InputData input, AnimationController anim)
        {
            base.EnterState(controller, input, anim);
            sideways = new Vector2Int(0, 0);
            vertical = new Vector2Int(0, 1);
            if (input.dir.x != 0) sideways = new Vector2Int(input.dir.x, 0);
            groundAware = false;
            currentStep = 0;
            tooFar = false;
            startPos = Vector2Int.RoundToInt(controller.gameObject.transform.position);
            if (input.dir.x != 0)
            {
                anim.Orient(input.dir.x);
            }
            anim.Jump();
            AudioManager.EmitOnce(AudioManager.Sound.Jump);
            IncrementalJump(controller);
        }

        public override void FixedUpdate(Controller player, InputHandler.InputData input)
        {
            IncrementalJump(player);
            anim.Jump();
        }

        public void IncrementalJump(Controller player)
        {
            if (player.IncrementalMove(sideways, 2, groundAware, false))
            {
                if (HasDied(player))
                {
                    player.Transition(player.FallingDeadState);
                    return;
                }
                else
                {
                    AudioManager.EmitOnce(AudioManager.Sound.Land);
                    player.Transition(player.IdleState);
                    return;
                }
            }

            if (player.IncrementalMove(vertical, Mathf.Abs(JumpInfo[currentStep]), groundAware, false))
            {
                if (HasDied(player))
                {
                    player.Transition(player.FallingDeadState);
                    return;
                }
                else
                {
                    AudioManager.EmitOnce(AudioManager.Sound.Land);
                    player.Transition(player.IdleState);
                    return;
                }
            }
            if (currentStep < JumpInfo.Length - 1) currentStep++;
            if (vertical.y > 0 && currentStep > 10) vertical = new Vector2Int(0, -1);
            if (!groundAware && currentStep > 10) groundAware = true;
            if (player.env.CanSlide)
            {
                player.Transition(player.SlidingState);
            }

            if (currentStep < 3) return;
            if (player.env.CanClimbUpRope || player.env.CanClimbDownRope)
            {
                player.Transition(player.ClimbingRopeState);
            }
        }

        public bool HasDied(Controller player)
        {
            Vector2Int curPos = Vector2Int.RoundToInt(player.gameObject.transform.position);
            int maxFall = startPos.y - 32;
            if (curPos.y < maxFall) return true;
            return false;
        }
    }
}