using UnityEngine;
using Com.Technitaur.GreenBean.Input;
using Com.Technitaur.GreenBean.Core;

namespace Com.Technitaur.GreenBean.Player
{
    public class Controller : MonoBehaviour, IKillable
    {
        
        [SerializeField] private AnimationController anim = null;

        public IEnvironment env;
        public RoomLoader.Direction leavingDir;

        public const int walkSpeed = 2;
        public const int fallSpeed = 4;
        public const int ladderClimbSpeed = 1;
        public const int ropeClimbUpSpeed = 1;
        public const int ropeClimbDownSpeed = 1;
        public const int beltSpeedModifier = 1;

        internal PlayerBaseState state;
        internal InputHandler inputHandler;
        public InputHandler.InputData input;


        public Idle IdleState = new Idle();
        public Walking WalkingState = new Walking();
        public Jumping JumpingState = new Jumping();
        public Falling FallingState = new Falling();
        public ClimbingLadder ClimbingLadderState = new ClimbingLadder();
        public ClimbingRope ClimbingRopeState = new ClimbingRope();
        public Sliding SlidingState = new Sliding();
        public Dead DeadState = new Dead();
        public FireDead FireDeadState = new FireDead();
        public FallingDead FallingDeadState = new FallingDead();

        public void Start()
        {
            inputHandler = GetComponent<InputHandler>();
            env = GetComponent<IEnvironment>();
            RoomLoader.lastSpawnPos = Vector2Int.RoundToInt(transform.position);
        }
        
        public void OnEnable()
        {
            Transition(IdleState);
        }
        
        public void DieInAFire()
        {
            Transition(FireDeadState);
        }
        
        
        public void Update()
        {
            input = inputHandler.GetData();
        }

        public void Transition(PlayerBaseState state)
        {
            state.EnterState(this, input, anim);
            this.state = state;
        }

        public void FixedUpdate()
        {
            state.FixedUpdate(this, input);
            if (env.IsOnFire && state != FireDeadState)
            {
                Transition(FireDeadState);
            }
        }
        
        // returns true if we get grounded before we've finished the sequence
        public bool IncrementalMove(Vector2Int dir, int upf, bool stopOnGrounded, bool stopOnNotGrounded)
        {
            for (int i = 0; i < upf; i++)
            {
                Vector3Int newPos = Vector3Int.RoundToInt(transform.position);
                if (!env.CanMoveRight && dir.x > 0 || !env.CanMoveLeft && dir.x < 0)
                {
                    newPos += new Vector3Int(0, dir.y, 0);
                }
                else
                {
                    newPos += (Vector3Int)dir;
                }
                transform.position = newPos;
                if (IsOutsideBounds(out leavingDir)) RoomLoader.Load(leavingDir, gameObject);
                if (env.IsGrounded && stopOnGrounded) return true;
                if (!env.IsGrounded && stopOnNotGrounded) return true;
            }
            return false;
        }
        
        private bool IsOutsideBounds(out RoomLoader.Direction dir)
        {
            if (!inputHandler.isActiveAndEnabled)
            {
                dir = RoomLoader.Direction.None;
                return false;
            }            
            var xBounds = 160;
            var yBounds = 112;
            if (transform.position.x >= xBounds)
            {
                dir = RoomLoader.Direction.Right;
                return true;
            }
            if (transform.position.x <= -xBounds)
            {
                dir = RoomLoader.Direction.Left;
                return true;
            }
            if (transform.position.y >= yBounds)
            {
                dir = RoomLoader.Direction.Up;
                return true;
            }
            if (transform.position.y <= -yBounds)
            {
                dir = RoomLoader.Direction.Down;
                return true;
            }
            dir = RoomLoader.Direction.None;
            return false;
        }

        public void Kill()
        {
            Transition(DeadState);
        }
    }
}
