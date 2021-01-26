using UnityEngine;
using Com.Technitaur.GreenBean.Input;

namespace Com.Technitaur.GreenBean.Player
{
    public class ClimbingLadder : PlayerBaseState
    {
        private int frame = 0;

        public override void EnterState(Controller player, InputHandler.InputData input, AnimationController anim)
        {
            base.EnterState(player, input, anim);
            Vector2Int rounded = Vector2Int.RoundToInt(player.transform.position);
            bool snapDir = player.env.LadderSnapRight;
            Vector2Int snapped = player.env.HorizontalSnap(rounded, snapDir);
            player.gameObject.transform.position = (Vector3Int)snapped;
            if (input.dir.y < 0 && player.env.CanClimbDownLadder)
            {
                player.gameObject.transform.position -= new Vector3(0, 4, 0);
            }
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
            if (player.env.OnMountingLadder)
            {
                anim.MountLadder();
            }
            else
            {
                anim.Climb(input.dir.y, true);
            }
        }
    }
}