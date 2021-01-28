using UnityEngine;
using Com.Technitaur.GreenBean.Input;
using Com.Technitaur.GreenBean.Core;

namespace Com.Technitaur.GreenBean.Player
{
    public class DeathState : PlayerBaseState
    {
        protected float reloadDelayPhaseOne;
        protected float reloadDelayPhaseTwo;

        public override void EnterState(Controller controller, InputHandler.InputData input, AnimationController anim)
        {
            base.EnterState(controller, input, anim);
            anim.Orient(1);
            Lives.Decrement();
        }

        public override void FixedUpdate(Controller player, InputHandler.InputData input)
        {
            if (reloadDelayPhaseOne > 0)
            {
                reloadDelayPhaseOne--;
                return;
            }
            anim.ClearSprite();
            GameStatus.DeclareDead();
            if (reloadDelayPhaseTwo > 0)
            {
                reloadDelayPhaseTwo--;
                return;
            }
            // TODO: Make this process a little nicer, we should probably have a global 'get this room'
            RoomLoader.ReloadCurrentRoom(player.gameObject);
        }
    }
}
