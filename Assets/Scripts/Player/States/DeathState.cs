using UnityEngine;
using Com.Technitaur.GreenBean.Input;
using Com.Technitaur.GreenBean.Core;

namespace Com.Technitaur.GreenBean.Player
{
    public class DeathState : PlayerBaseState
    {
        private float reloadDelay;

        public override void EnterState(Controller controller, InputHandler.InputData input)
        {
            Lives.Decrement();
            reloadDelay = 4;
        }

        public override void FixedUpdate(Controller player, InputHandler.InputData input)
        {
            if (reloadDelay > 0)
            {
                reloadDelay -= Time.deltaTime;
                return;
            }
            // TODO: Make this process a little nicer, we should probably have a global 'get this room'
            RoomLoader.Reload(GameObject.FindObjectOfType<RoomData>().RoomName, player.gameObject);
        }
    }
}
