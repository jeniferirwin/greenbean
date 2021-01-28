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
                Debug.Log("Reached the breakpoint");
                _intro.input.IntroState(false, false);
                breakpoint = true;
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
