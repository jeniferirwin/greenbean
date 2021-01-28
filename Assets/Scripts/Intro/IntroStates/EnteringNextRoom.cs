using UnityEngine;

namespace Com.Technitaur.GreenBean.Intro
{
    public class EnteringNextRoom : IntroBaseState
    {
        bool breakpoint = false;

        internal override void FixedUpdate()
        {
            if (breakpoint) _intro.enabled = false;
            if (_player.transform.position.y < 64)
            {
                _intro.input.IntroState(false, false);
                breakpoint = true;
                _player.anim.Orient(-1);
                _player.SetLastEnteredState(_player.IdleState);
                _player.Transition(_player.FallingState);
            }
            else if (_player.state != _player.ClimbingRopeState && _player.env.CanClimbDownRope)
            {
                _intro.input.IntroState(true, true);
                _player.Transition(_player.ClimbingRopeState);
            }
        }
    }
}
