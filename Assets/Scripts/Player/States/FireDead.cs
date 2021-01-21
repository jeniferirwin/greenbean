using UnityEngine;
using Com.Technitaur.GreenBean.Input;
using Com.Technitaur.GreenBean.Core;

namespace Com.Technitaur.GreenBean.Player
{
    public class FireDead : PlayerBaseState
    {
        public override void FixedUpdate(Controller controller, InputHandler.InputData input)
        {
        }
        public override void EnterState(Controller controller, InputHandler.InputData input)
        {
            Debug.Log("Player died in a fire.");
            Lives.Decrement();
        }
    }
}