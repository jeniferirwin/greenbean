using UnityEngine;
using UnityEngine.InputSystem;
using Com.Technitaur.GreenBean.Input;
using Com.Technitaur.GreenBean.Core;
using Com.Technitaur.GreenBean.Player;
using Com.Technitaur.GreenBean.Interactables;
using Com.Technitaur.GreenBean.Tilemaps;

namespace Com.Technitaur.GreenBean.Intro
{
    public class IntroAnimator : MonoBehaviour
    {
        public PersistentCycleKeeper cycle = null;
        public InputHandler input = null;

        [SerializeField] private Controller _player = null;
        [SerializeField] private IEnvironment _env = null;

        private IntroBaseState state;
        internal int jumps = 4;
        internal bool waiting;

        internal SlidingDownPole SlidingDownPoleState = new SlidingDownPole();
        internal Jump JumpState = new Jump();
        internal Waiting WaitingState = new Waiting();
        internal ClimbingDownRope ClimbingDownRopeState = new ClimbingDownRope();
        internal EnteringNextRoom EnteringNextRoomState = new EnteringNextRoom();

        private bool gameStarted;

        private void OnDisable()
        {
            input.IntroState(false, false);
            RoomLoader.lastSpawnPos = new Vector2Int(4, 32);
        }

        private void Awake()
        {
            cycle = GameObject.FindObjectOfType<PersistentCycleKeeper>();
        }

        private void OnEnable()
        {
            int num;
            char grade;
            (grade, num) = RoomLoader.GetCurrentRoomInfo();
            if (grade == 'A' && num == 10 && !gameStarted)
            {
                gameStarted = true;
                Transition(EnteringNextRoomState);
            }
        }

        public void StartGame(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                if (state == WaitingState && waiting == true)
                {
                    waiting = false;
                }
            }
        }

        private void Start()
        {
            gameStarted = false;
            input.IntroState(true, false);
            transform.position = new Vector2(-148, 46);
            waiting = true;
            _env = GetComponent<IEnvironment>();
            if (_env == null)
            {
                Debug.Log("Environment not found.");
                return;
            }
            TileStopper.StopTiles();
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
