using UnityEngine;
using Com.Technitaur.GreenBean.Input;

namespace Com.Technitaur.GreenBean.Player
{
    public class PlayerBaseState
    {
        protected AnimationController anim;

        public virtual void FixedUpdate(Controller controller, InputHandler.InputData input)
        {

        }
        public virtual void EnterState(Controller controller, InputHandler.InputData input, AnimationController anim)
        {
            this.anim = anim;
            anim.ResetTicks();
        }
    }
}