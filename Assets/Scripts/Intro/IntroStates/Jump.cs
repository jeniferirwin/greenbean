using UnityEngine;
using Com.Technitaur.GreenBean.Input;
using Com.Technitaur.GreenBean.Player;
using Com.Technitaur.GreenBean.Core;

namespace Com.Technitaur.GreenBean.Intro
{
    public class Jump : IntroBaseState
    {
        internal override void EnterState(Controller player, IntroAnimator intro)
        {
            AudioManager.isMuted = false;
            base.EnterState(player, intro);
            _player.input = GenerateInput();
            _player.Transition(player.JumpingState);
        }
        
        private InputHandler.InputData GenerateInput()
        {
            var input = new InputHandler.InputData();
            input.dir = new Vector2Int(1, 0);
            input.jump = true;
            return input;
        }
        
        internal override void FixedUpdate()
        {
            if (!_player.env.IsGrounded) return;
            else
            {
                if (_intro.jumps <= 1)         
                {
                    _intro.Transition(_intro.ClimbingDownRopeState);
                }
                else if (_intro.jumps == 4)
                {
                    _intro.Transition(_intro.WaitingState);
                }
                else
                {
                    _intro.jumps--;
                    _intro.Transition(_intro.JumpState);
                }
            }
        }
    }
}