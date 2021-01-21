using UnityEngine;
using Com.Technitaur.GreenBean.Input;
using Com.Technitaur.GreenBean.Core;

namespace Com.Technitaur.GreenBean.Player
{
    public class FallingDead : PlayerBaseState
    {
        public override void FixedUpdate(Controller controller, InputHandler.InputData input)
        {
        }
        public override void EnterState(Controller controller, InputHandler.InputData input)
        {
            Debug.Log("Player was killed by falling.");
            Lives.Decrement();
        }
    }
}
