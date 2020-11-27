using UnityEngine;
using Com.Technitaur.GreenBean.Input;

namespace Com.Technitaur.GreenBean.Player
{
    public class ClimbingLadder : PlayerBaseState
    {
        public override void EnterState(Controller player, InputHandler.InputData input)
        {
            Vector2Int rounded = Vector2Int.RoundToInt(player.transform.position);
            bool snapDir = player.env.LadderSnapRight;
            Vector2Int snapped = player.env.HorizontalSnap(rounded, snapDir);
            player.gameObject.transform.position = (Vector3Int)snapped;
        }

        public override void FixedUpdate(Controller player, InputHandler.InputData input)
        {
            if (input.dir.y > 0)
            {
                if (player.env.CanClimbUpLadder)
                {
                    player.IncrementalMove(Vector2Int.up, 1, false, false);
                    return;
                }
                else
                {
                    player.Transition(player.IdleState);
                    return;
                }
            }
            if (input.dir.y < 0)
            {
                if (player.env.CanClimbDownLadder)
                {
                    player.IncrementalMove(Vector2Int.down, 1, false, false);
                    return;
                }
                else
                {
                    player.Transition(player.IdleState);
                    return;
                }
            }
        }
    }
}