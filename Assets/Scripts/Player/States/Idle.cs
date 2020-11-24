using UnityEngine;
using Com.Technitaur.GreenBean.Input;

namespace Com.Technitaur.GreenBean.Player
{
    public class Idle : PlayerBaseState
    {
        public override void EnterState(Controller player, InputHandler.InputData input)
        {
        }

        public override void FixedUpdate(Controller player, InputHandler.InputData input)
        {
            if (!player.env.IsGrounded)
            {
                Debug.Log("Falling now");
                player.Transition(player.FallingState);
            }

            if (input.jump && player.env.IsGrounded) player.Transition(player.JumpingState);

            if (input.dir.y > 0 && player.env.CanClimbUpLadder) player.Transition(player.ClimbingLadderState);
            if (input.dir.y < 0 && player.env.CanClimbDownLadder) player.Transition(player.ClimbingLadderState);

            if (input.dir.y > 0 && player.env.CanClimbUpRope) player.Transition(player.ClimbingRopeState);
            if (input.dir.y < 0 && player.env.CanClimbDownRope) player.Transition(player.ClimbingRopeState);

            if (input.dir.x != 0) player.Transition(player.WalkingState);

            if (player.env.IsOnLeftBelt && player.env.CanMoveLeft)
            {
                if (player.IncrementalMove(Vector2Int.left, 1, false, true))
                    player.Transition(player.FallingState);
            }

            if (player.env.IsOnRightBelt && player.env.CanMoveRight)
            {
                if (player.IncrementalMove(Vector2Int.right, 1, false, true))
                    player.Transition(player.FallingState);
            }
        }
    }
}