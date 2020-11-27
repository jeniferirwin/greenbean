﻿using UnityEngine;
using Com.Technitaur.GreenBean.Input;

namespace Com.Technitaur.GreenBean.Player
{
    public class Walking : PlayerBaseState
    {
        public override void EnterState(Controller controller, InputHandler.InputData input)
        {

        }

        public override void FixedUpdate(Controller player, InputHandler.InputData input)
        {
            if (input.dir.x == 0)
            {
                player.Transition(player.IdleState);
                return;
            }

            if (input.jump)
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

            Vector2Int dir = new Vector2Int(input.dir.x, 0);
            if (player.IncrementalMove(input.dir, 2, false, true))
            {
                player.Transition(player.FallingState);
                return;
            }
        }
    }
}