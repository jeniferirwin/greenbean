using UnityEngine;

namespace Com.Technitaur.GreenBean.Intro
{
    public class ClimbingDownRope : IntroBaseState
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
