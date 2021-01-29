using UnityEngine;
using Com.Technitaur.GreenBean.Input;

namespace Com.Technitaur.GreenBean.Player
{
    public class Sliding : PlayerBaseState
    {
        public override void EnterState(Controller player, InputHandler.InputData input, AnimationController anim)
        {
            base.EnterState(player, input, anim);
            var loc = Vector2Int.RoundToInt(player.gameObject.transform.position);
            player.gameObject.transform.position = (Vector2) player.env.CenterXYSnap(loc);
            player.anim.Orient(-1);
        }

        public override void FixedUpdate(Controller player, InputHandler.InputData input)
        {
            anim.Slide();
            if (player.IncrementalMove(Vector2Int.down, 2, true, false))
            {
                player.Transition(player.IdleState);
            }
        }
    }
}