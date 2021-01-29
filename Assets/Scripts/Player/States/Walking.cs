using UnityEngine;
using Com.Technitaur.GreenBean.Core;
using Com.Technitaur.GreenBean.Input;

namespace Com.Technitaur.GreenBean.Player
{
    public class Walking : PlayerBaseState
    {
        private int lastX = 0;

        public override void EnterState(Controller controller, InputHandler.InputData input, AnimationController anim)
        {
            base.EnterState(controller, input, anim);
            lastX = input.dir.x;
            anim.Orient(input.dir.x);
            AudioManager.EmitContinuous(AudioManager.Sound.Footsteps);
        }

        public override void FixedUpdate(Controller player, InputHandler.InputData input)
        {
            if (input.dir.x == 0)
            {
                AudioManager.StopEmitting();
                player.Transition(player.IdleState);
                return;
            }

            if (input.jump)
            {
                AudioManager.StopEmitting();
                player.Transition(player.JumpingState);
                return;
            }

            if (input.dir.y > 0 && player.env.CanClimbUpLadder)
            {
                AudioManager.StopEmitting();
                player.Transition(player.ClimbingLadderState);
                return;
            }
            if (input.dir.y < 0 && player.env.CanClimbDownLadder)
            {
                AudioManager.StopEmitting();
                player.Transition(player.ClimbingLadderState);
                return;
            }
            if (input.dir.y > 0 && player.env.CanClimbUpRope)
            {
                AudioManager.StopEmitting();
                player.Transition(player.ClimbingRopeState);
                return;
            }
            if (input.dir.y < 0 && player.env.CanClimbDownRope)
            {
                AudioManager.StopEmitting();
                player.Transition(player.ClimbingRopeState);
                return;
            }

            Vector2Int dir = new Vector2Int(input.dir.x, 0);
            int speed = 2;
            if (dir.x < 0 && player.env.IsOnLeftBelt) speed = 3;
            if (dir.x < 0 && player.env.IsOnRightBelt) speed = 1;
            if (dir.x > 0 && player.env.IsOnLeftBelt) speed = 1;
            if (dir.x > 0 && player.env.IsOnRightBelt) speed = 3;
            if (player.IncrementalMove(dir, speed, false, true))
            {
                AudioManager.StopEmitting();
                player.Transition(player.FallingState);
                return;
            }
            if (input.dir.x != lastX)
            {
                anim.Orient(input.dir.x);
                lastX = input.dir.x;
            }
            anim.Walk();
        }
    }
}