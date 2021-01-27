using UnityEngine;
using UnityEngine.InputSystem;
using Com.Technitaur.GreenBean.Input;
using Com.Technitaur.GreenBean.Core;
using Com.Technitaur.GreenBean.Player;
using Com.Technitaur.GreenBean.Interactables;

namespace Com.Technitaur.GreenBean.Intro
{
    public class IntroAnimator : MonoBehaviour
    {
        private enum Phase
        {
            SlidingDown,
            FirstJump,
            WaitingForStart,
            SecondJump,
            ThirdJump,
            FourthJump,
            GoingDownRope
        }

        public PersistentCycleKeeper cycle = null;

        [SerializeField] private InputHandler _input = null;
        [SerializeField] private Controller _player = null;
        [SerializeField] private IEnvironment _env = null;

        private IntroBaseState state;
        internal int jumps = 2;
        internal bool waiting;

        internal SlidingDownPole SlidingDownPoleState = new SlidingDownPole();
        internal Jump JumpState = new Jump();
        internal Waiting WaitingState = new Waiting();
        internal ClimbingDownRope ClimbingDownRopeState = new ClimbingDownRope();

        private void OnDisable() => _input.IntroState(false);

        private void Awake()
        {
            cycle = GameObject.FindObjectOfType<PersistentCycleKeeper>();
        }

        public void StartGame(InputAction.CallbackContext context)
        {
            Debug.Log("Start pressed.");
            if (context.started)
            {
                if (state == WaitingState)
                {
                    waiting = false;
                }
            }
        }

        private void Start()
        {
            _input.IntroState(true);
            transform.position = new Vector2(-148, 100);
            waiting = true;
            _env = GetComponent<IEnvironment>();
            if (_env == null)
            {
                Debug.Log("Environment not found.");
                return;
            }
            Transition(SlidingDownPoleState);
        }

        internal void Transition(IntroBaseState state)
        {
            this.state = state;
            state.EnterState(_player, this);
        }

        private void FixedUpdate()
        {
            state.FixedUpdate();
        }
    }
}
