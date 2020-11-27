using UnityEngine;
using Com.Technitaur.GreenBean.Input;

namespace Com.Technitaur.GreenBean.Player
{
    public class ClimbingLadder : PlayerBaseState
    {
        public override void EnterState(Controller controller, InputHandler.InputData input)
        {
            Vector2Int rounded = Vector2Int.RoundToInt(controller.transform.position);
            bool snapDir = controller.env.LadderSnapRight;
            Vector2Int snapped = controller.env.HorizontalSnap(rounded, snapDir);
            controller.gameObject.transform.position = (Vector3Int) snapped;
        }
        
        public override void FixedUpdate(Controller player, InputHandler.InputData input)
        {
            if (input.dir.y > 0 && player.env.CanClimbUpLadder)
            {
                if (player.IncrementalMove(Vector2Int.up, 1, true, false))
                {
                    player.Transition(player.IdleState);
                    return;
                }
            }
            else if (input.dir.y < 0 && player.env.CanClimbDownLadder)
            {
                if (player.IncrementalMove(Vector2Int.down, 1, true, true))
                {
                    if (player.env.IsGrounded)
                    {
                        player.Transition(player.IdleState);
                        return;
                    }
                    if (!player.env.CanClimbDownLadder)
                    {
                        player.Transition(player.FallingState);
                        return;
                    }
                }
            }
        }
    }
}