using UnityEngine;
using Com.Technitaur.GreenBean.Input;
using Com.Technitaur.GreenBean.Core;

namespace Com.Technitaur.GreenBean.Player
{
    public class FireDead : DeathState
    {
        public override void EnterState(Controller controller, InputHandler.InputData input, AnimationController anim)
        {
            base.EnterState(controller, input, anim);
            reloadDelayPhaseOne = 63;
            reloadDelayPhaseTwo = 65;
            anim.FireDeath();
        }
    }
}