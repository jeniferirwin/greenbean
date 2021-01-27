using UnityEngine;
using Com.Technitaur.GreenBean.Input;
using Com.Technitaur.GreenBean.Core;

namespace Com.Technitaur.GreenBean.Player
{
    public class Dead : DeathState
    {
        public override void EnterState(Controller controller, InputHandler.InputData input, AnimationController anim)
        {
            base.EnterState(controller, input, anim);
            reloadDelayPhaseOne = 31;
            reloadDelayPhaseTwo = 65;
        }
        
        public override void FixedUpdate(Controller player, InputHandler.InputData input)
        {
            anim.MobDeath();
            base.FixedUpdate(player, input);
        }
    }
}
