using UnityEngine;
using Com.Technitaur.GreenBean.Core;

namespace Com.Technitaur.GreenBean.Interactables
{
    public abstract class Mob : Trackable, IKillable
    {
        public bool CanDieToSword { get { return canDieToSword; } }

        [SerializeField] private float ppf;
        [SerializeField] private bool canDieToSword;
        [SerializeField] private Vector2Int startDirection;
        [SerializeField] private IEnvironment _env;

        private Vector2Int currentDirection;
        
        public virtual void Start()
        {
            _env = GetComponent<IEnvironment>();
        }
        
        public virtual void FixedUpdate()
        {
            Move(currentDirection);
        }
        
        public virtual void Move(Vector2Int dir)
        {
            for (int i = 0; i <= ppf; i++)
            {
                Vector2 lastPos = transform.position;
                transform.position += (Vector3) (Vector2) dir;
                if (IsBlocked())
                {
                    transform.position = lastPos;
                    return;
                }
            }
        }
        
        public virtual bool IsBlocked()
        {
            if (!_env.IsGrounded)
            {
                currentDirection.x = -currentDirection.x;
                return true;
            }
            if (currentDirection.x > 0 && !_env.CanMoveRight)
            {
                currentDirection.x = -1;
                return true;
            }
            if (currentDirection.x < 0 && !_env.CanMoveLeft)
            {
                currentDirection.x = 1;
                return true;
            }
            // do extra stuff with y for spiders here later
            return false;
        }

        public void Kill()
        {
            SetDirty();
        }

        public override void SetDirty()
        {
            gameObject.SetActive(false);
        }

        public override void SetClean()
        {
            currentDirection = startDirection;
        }
    }
}
