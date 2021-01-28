using UnityEngine;
using Com.Technitaur.GreenBean.Interactables;
using Com.Technitaur.GreenBean.Tilemaps;
using Com.Technitaur.GreenBean.Player;

namespace Com.Technitaur.GreenBean.Intro
{
    public class Waiting : IntroBaseState
    {
        internal override void EnterState(Controller player, IntroAnimator intro)
        {
            base.EnterState(player, intro);
            StartFlasher.StartFlashing();
        }

        internal override void FixedUpdate()
        {
            if (_intro.waiting) return;
            else
            {
                _intro.cycle.StartCycle();
                TileStopper.StartTiles();
                StartFlasher.StopFlashing();
                _intro.jumps--;
                _intro.Transition(_intro.JumpState);
            }
        }
    }
}
