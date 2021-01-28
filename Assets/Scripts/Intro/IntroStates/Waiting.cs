using UnityEngine;
using Com.Technitaur.GreenBean.Interactables;

namespace Com.Technitaur.GreenBean.Intro
{
    public class Waiting : IntroBaseState
    {
        internal override void FixedUpdate()
        {
            if (_intro.waiting) return;
            else
            {
                _intro.cycle.StartCycle();
                _intro.jumps--;
                _intro.Transition(_intro.JumpState);
            }
        }
    }
}
