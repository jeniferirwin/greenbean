using UnityEngine;
using Com.Technitaur.GreenBean.Input;

namespace Com.Technitaur.GreenBean.Player
{
    public class Idle : PlayerBaseState
    {
        public override void EnterState(Controller player, InputHandler.InputData input, AnimationController anim)
        {
            base.EnterState(player, input, anim);
            anim.Idle();
        }

        public override void FixedUpdate(Controller player, InputHandler.InputData input)
        {
            if (!player.env.IsGrounded)
            {
                player.Transition(player.FallingState);
                return;
            }

            if (input.jump && player.env.IsGrounded)
            {
                player.Transition(player.JumpingState);
                return;
            }

            if (input.dir.y > 0 && player.env.CanClimbUpLadder)
            {
                player.Transition(player.ClimbingLadderState);
                return;
            }

            if (input.dir.y < 0 && player.env.CanClimbDownLadder)
            {
                player.Transition(player.ClimbingLadderState);
                return;
            }

            if (input.dir.y > 0 && player.env.CanClimbUpRope)
            {
                player.Transition(player.ClimbingRopeState);
                return;
            }
            if (input.dir.y < 0 && player.env.CanClimbDownRope)
            {
                player.Transition(player.ClimbingRopeState);
                return;
            }

            if (input.dir.x != 0)
            {
                player.Transition(player.WalkingState);
                return;
            }

            if (player.env.IsOnLeftBelt && player.env.CanMoveLeft)
            {
                if (player.IncrementalMove(Vector2Int.left, 1, false, true))
                {
                    player.Transition(player.FallingState);
                    return;
                }
            }

            if (player.env.IsOnRightBelt && player.env.CanMoveRight)
            {
                if (player.IncrementalMove(Vector2Int.right, 1, false, true))
                {
                    player.Transition(player.FallingState);
                    return;
                }
            }
            
            if (player.env.CanClimbDownRope && input.dir.y < 0)
            {
                player.Transition(player.ClimbingRopeState);
            }
        }
    }
}