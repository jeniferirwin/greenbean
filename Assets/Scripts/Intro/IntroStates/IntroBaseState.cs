using UnityEngine;
using Com.Technitaur.GreenBean.Player;

namespace Com.Technitaur.GreenBean.Intro
{
    public class IntroBaseState
    {
        protected Controller _player;
        protected IntroAnimator _intro;
        
        internal virtual void EnterState(Controller player, IntroAnimator intro)
        {
            _player = player;
            _intro = intro;
        }

        internal virtual void FixedUpdate() {}
    }
}
