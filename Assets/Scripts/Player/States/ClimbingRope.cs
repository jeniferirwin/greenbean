using UnityEngine;
using Com.Technitaur.GreenBean.Input;

namespace Com.Technitaur.GreenBean.Player
{
    public class ClimbingRope : PlayerBaseState
    {
        private Vector2Int startedClimbing;
        private bool frameSkip;

        public override void EnterState(Controller controller, InputHandler.InputData input)
        {
            // TODO: add ladder snap through ISensor
            startedClimbing = Vector2Int.RoundToInt(controller.transform.position);
        }

        public override void FixedUpdate(Controller player, InputHandler.InputData input)
        {
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
            else return;

            if (player.IncrementalMove(yaxis, 1, true, true))
            {
                if (player.env.IsGrounded) player.Transition(player.IdleState);
                else player.Transition(player.FallingState);
            }
        }
    }
}