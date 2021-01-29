using UnityEngine;
using Com.Technitaur.GreenBean.Input;
using Com.Technitaur.GreenBean.Core;

namespace Com.Technitaur.GreenBean.Player
{
    public class FallingDead : DeathState
    {
        public override void EnterState(Controller controller, InputHandler.InputData input, AnimationController anim)
        {
            base.EnterState(controller, input, anim);
            reloadDelayPhaseOne = 62;
            reloadDelayPhaseTwo = 65;
            AudioManager.EmitOnce(AudioManager.Sound.Land);
        }

        public override void FixedUpdate(Controller player, InputHandler.InputData input)
        {
            base.FixedUpdate(player, input);
            if (reloadDelayPhaseOne > 0) anim.FallDeath();
        }
    }
}
