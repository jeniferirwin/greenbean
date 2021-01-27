using UnityEngine;

namespace Com.Technitaur.GreenBean.Player
{
    public class Waiting : IntroBaseState
    {
        internal override void FixedUpdate()
        {
            if (!_player.env.IsGrounded) return;
            else
            {
                _intro.Transition(_intro.JumpState);
            }
        }
    }
}
