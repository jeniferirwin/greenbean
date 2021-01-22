using UnityEngine;
using Com.Technitaur.GreenBean.Input;
using System;

namespace Com.Technitaur.GreenBean.Player
{
    public class ClimbingRope : PlayerBaseState
    {
        private Vector2Int startedClimbing;
        private bool frameSkip;

        public override void EnterState(Controller controller, InputHandler.InputData input)
        {
            var pos = controller.gameObject.transform.position;
            var rounded = Vector2Int.RoundToInt(pos);
            controller.gameObject.transform.position = (Vector2) controller.env.CenterXSnap(rounded);
        }

        public override void FixedUpdate(Controller player, InputHandler.InputData input)
        {
            CheckForJumpOff(player, input);
            if (frameSkip)
            {
                // in the original game, he stays at each y position for two frames
                // while climbing up ropes
                frameSkip = false;
                return;
            }

            Vector2Int yaxis = Vector2Int.zero;
            if (input.dir.y > 0)
            {
                yaxis = Vector2Int.up;
                frameSkip = true;
            }

            if (input.dir.y < 0) yaxis = Vector2Int.down;

            if (player.IncrementalMove(yaxis, 1, true, true))
            {
                if (player.env.CanClimbDownRope && player.env.CanClimbUpRope) return;

                if (player.env.IsGrounded)
                {
                    player.Transition(player.IdleState);
                    return;
                }
                else 
                {
                    player.Transition(player.FallingState);
                    return;
                }
            }
        }

        private void CheckForJumpOff(Controller player, InputHandler.InputData input)
        {
            if (input.dir.x != 0 && input.jump)
            {
                player.Transition(player.JumpingState);
            }
        }
    }
}