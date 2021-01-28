using Com.Technitaur.GreenBean.Player;
using UnityEngine;

namespace Com.Technitaur.GreenBean.Intro
{
    public class SlidingDownPole : IntroBaseState
    {
        internal override void EnterState(Controller player, IntroAnimator intro)
        {
            base.EnterState(player, intro);
            player.Transition(player.SlidingState);
        }
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
