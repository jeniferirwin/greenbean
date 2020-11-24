using Com.Technitaur.GreenBean.Input;

namespace Com.Technitaur.GreenBean.Player
{
    public abstract class PlayerBaseState
    {
        public abstract void FixedUpdate(Controller controller, InputHandler.InputData input);
        public abstract void EnterState(Controller controller, InputHandler.InputData input);
    }
}