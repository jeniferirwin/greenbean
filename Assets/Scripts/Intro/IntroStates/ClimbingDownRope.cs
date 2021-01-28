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
                if (_player.input.dir.y == 0)
                {
                    Debug.Log("Wanna climb rope");
                    _intro.input.IntroState(true, true);
                    _player.input.dir.y = -1;
                    _player.Transition(_player.ClimbingRopeState);
                }
            }
        }
    }
}
