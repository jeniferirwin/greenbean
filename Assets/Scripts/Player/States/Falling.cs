using UnityEngine;
using Com.Technitaur.GreenBean.Input;
using Com.Technitaur.GreenBean.Core;

namespace Com.Technitaur.GreenBean.Player
{
    public class Falling : PlayerBaseState
    {
        private Vector3Int startingPosition;

        public override void EnterState(Controller player, InputHandler.InputData input, AnimationController anim)
        {
            base.EnterState(player, input, anim);
            startingPosition = Vector3Int.RoundToInt(player.gameObject.transform.position);
            player.SetLastSpriteRotation();
        }

        public override void FixedUpdate(Controller player, InputHandler.InputData input)
        {
            if (player.IncrementalMove(Vector2Int.down, Controller.fallSpeed, true, false))
            {
                if (player.transform.position.y < startingPosition.y - 32)
                {
                    player.Transition(player.FallingDeadState);
                    return;
                }
                else
                {
                    AudioManager.EmitOnce(AudioManager.Sound.Land);
                    player.Transition(player.IdleState);
                    return;
                }
            }
        }
    }
}
