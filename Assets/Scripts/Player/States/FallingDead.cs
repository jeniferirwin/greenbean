using UnityEngine;
using Com.Technitaur.GreenBean.Input;
using Com.Technitaur.GreenBean.Core;

namespace Com.Technitaur.GreenBean.Player
{
    public class FallingDead : DeathState
    {
        public override void FixedUpdate(Controller player, InputHandler.InputData input)
        {
            base.FixedUpdate(player, input);
            if (reloadDelayPhaseOne > 0) anim.FallDeath();
        }
    }
}
