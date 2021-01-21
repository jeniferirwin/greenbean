using UnityEngine;
using Com.Technitaur.GreenBean.Input;
using Com.Technitaur.GreenBean.Core;

namespace Com.Technitaur.GreenBean.Player
{
    public class Dead : PlayerBaseState
    {
        public override void EnterState(Controller controller, InputHandler.InputData input)
        {

        }

        public override void FixedUpdate(Controller player, InputHandler.InputData input)
        {
            Debug.Log("Player was killed.");
            Lives.Decrement();
        }
    }
}
