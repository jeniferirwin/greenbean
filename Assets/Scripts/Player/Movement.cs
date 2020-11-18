using UnityEngine;
using UnityEngine.InputSystem;
using GreenBean.Helpers;
using GreenBean.InputHandling;
using GreenBean.C64;

namespace GreenBean.Player
{
    public class Movement : MonoBehaviour
    {
        [Header("Setup")]
        public Rigidbody2D rb;
        public InputHandler inputHandler;
        public EnvironmentCheck envCheck;
        public GameObject wallCastOrigin;
        [Header("Climbing")]
        public bool canClimbLadder;
        public bool canClimbRope;

        public bool onLadder;
        public bool onRope;
        public bool onPole;

        [Header("Debug")]
        public bool isAtLadder;
        public bool isAtRope;
        public bool hasLadderAbove;
        public bool hasLadderBelow;
        
        public bool onLeftBelt;
        public bool onRightBelt;

        private JumpData jumpData;
        private Vector2 moveDirection;
        private Vector2 initPosition;

        private Vector2 walkVector = new Vector2(2f / 8f, 0f);
        private Vector2 fallVector = new Vector2(0f, -4f / 8f);
        private Vector2 falseVector = new Vector2(-32500f, 32500f);

        private void Start()
        {
            jumpData = null;
            rb.gravityScale = 0;
            initPosition = transform.position;
        }

        public void Reset(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                transform.position = initPosition;
                jumpData = null;
                rb.gravityScale = 0;
            }
        }

        private void FixedUpdate()
        {
            envCheck.LastPosition = transform.position;
            GetInputInfo();
            if (onLadder || onPole || jumpData != null)
                inputHandler.desiredJump = false;

            hasLadderAbove = envCheck.LadderAbove;
            hasLadderBelow = envCheck.LadderBelow;
            onLeftBelt = envCheck.OnLeftBelt;
            onRightBelt = envCheck.OnRightBelt;

            if (onLadder)
            {
                ProcessLadderClimbing();
                return;
            }

            if (onRope)
            {
                ProcessRopeClimbing();
                return;
            }

            if (onPole)
            {
                ProcessPoleSliding();
                return;
            }

            if (envCheck.Grounded && jumpData == null)
            {
                ProcessGroundState();
                return;
            }
            else
            {
                ProcessAirState();
            }
        }

        private void ProcessLadderClimbing()
        {
            if (moveDirection.y < 0)
            {
                if (envCheck.LadderBelow)
                {
                    transform.position = (Vector2)transform.position + (Vector2.down * PixConvert.PixelsToUnits(1));
                }
                else
                {
                    onLadder = false;
                }
            }
            if (moveDirection.y > 0)
            {
                if (envCheck.LadderAbove)
                {
                    transform.position = (Vector2)transform.position + (Vector2.up * PixConvert.PixelsToUnits(1));
                }
                else
                {
                    onLadder = false;
                }
            }
        }

        private void ProcessRopeClimbing()
        {

        }
        private void ProcessPoleSliding()
        {

        }

        private void GetInputInfo()
        {
            if (inputHandler.canGetValues)
            {
                inputHandler.canGetValues = false;
                moveDirection = inputHandler.desiredDirection;

                if (inputHandler.desiredJump && envCheck.Grounded && !onLadder)
                {
                    inputHandler.desiredJump = false;
                    InitiateJump(moveDirection);
                    return;
                }
            }
        }

        private void ProcessGroundState()
        {
            VerticalSnap();
            if (moveDirection.y != 0)
            {
                if (envCheck.LadderAbove && moveDirection.y > 0 || envCheck.LadderBelow && moveDirection.y < 0)
                {
                    HorizontalSnap();
                    onLadder = true;
                }
            }

            if (onLadder || onRope)
                return;

            if (moveDirection != Vector2.zero)
            {
                Vector2 lateralDir = moveDirection;
                lateralDir.y = 0;
                Vector2 change = lateralDir * PixConvert.PixelsToUnits(2);
                if (envCheck.OnRightBelt)
                    change.x += PixConvert.PixelsToUnits(1);
                else if (envCheck.OnLeftBelt)
                    change.x -= PixConvert.PixelsToUnits(1);
                change = SetBlocks(change);
                Vector2 newPosition = (Vector2)transform.position + change;
                transform.position = newPosition;
                return;
            }
            
            if (moveDirection == Vector2.zero && (envCheck.OnLeftBelt || envCheck.OnRightBelt))
            {
                Vector2 change = new Vector2(0,0);
                if (envCheck.OnRightBelt)
                    change.x += PixConvert.PixelsToUnits(1);
                else if (envCheck.OnLeftBelt)
                    change.x -= PixConvert.PixelsToUnits(1);
                change = SetBlocks(change);
                Vector2 newPosition = (Vector2)transform.position + change;
                transform.position = newPosition;
                
            }
        }

        private void VerticalSnap()
        {
            float nearestWhole = Mathf.Round(transform.position.y);
            transform.position = new Vector2(transform.position.x, nearestWhole);
        }

        private void HorizontalSnap()
        {
            float nearestWhole = Mathf.Round(transform.position.x);
            transform.position = new Vector2(nearestWhole, transform.position.y);
        }

        private Vector2 SetBlocks(Vector2 change)
        {
            float wantX = change.x;
            if (wantX > 0 && envCheck.RightBlocked || wantX < 0 && envCheck.LeftBlocked)
            {
                change.x = 0;
            }
            return change;
        }

        private void ProcessAirState()
        {
            if (jumpData != null)
            {
                MoveJumpStep(jumpData);
            }
            else
            {
                Vector2 dest = (Vector2)transform.position + (Vector2.down * 4 / 8f);
                Vector2 collPoint = envCheck.CollisionIntersect(transform.position, dest);
                if (collPoint == falseVector)
                {
                    transform.position = dest;
                }
                else
                {
                    transform.position = collPoint;
                    VerticalSnap(); // TODO: This might cause problems, must evaluate
                }
            }
        }

        private void InitiateJump(Vector2 dir)
        {
            jumpData = new JumpData(dir.x);
            MoveJumpStep(jumpData);
        }

        private void MoveJumpStep(JumpData data)
        {
            Vector2 change = data.GetNextChange();
            change = SetBlocks(change);
            Vector2 dest = (Vector2)transform.position + change;
            Debug.Log(dest);

            if (!jumpData.hasPeaked)
            {
                transform.position = dest;
                return;
            }

            Vector2 collPoint = envCheck.CollisionIntersect(transform.position, dest);
            if (collPoint == falseVector)
            {
                transform.position = dest;
            }
            else
            {
                transform.position = collPoint;
                jumpData = null;
            }
        }

        private void OnDrawGizmos()
        {
            /*
            Gizmos.DrawWireSphere(wallCastOrigin.transform.position, PixConvert.PixelsToUnits(1));
            */
        }
    }
}