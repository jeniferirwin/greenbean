using UnityEngine;
using Com.Technitaur.GreenBean.Core;
using System;

namespace Com.Technitaur.GreenBean.Interactables
{
    public enum MobType
    {
        RollingSkull,
        FastRollingSkull,
        BouncingSkull,
        FastBouncingSkull,
        Spider,
        FastSpider,
        Snake
    }

    public abstract class Mob : Trackable, IKillableMob
    {
        public bool CanDieToSword { get { return canDieToSword; } }
        public int PointValue { get { return pointValue; } }
        public MobType MobType { get { return mobType; } }
        public bool Faded { get { return faded; } }

        [SerializeField] private MobType mobType;
        [SerializeField] private float ppf;
        [SerializeField] private bool canDieToSword;
        [SerializeField] private int pointValue;
        [SerializeField] private Vector2Int startDirection;
        [SerializeField] private IEnvironment _env;
        [SerializeField] private GameObject _spriteContainer;
        [SerializeField] private Color fadedColor;
        [SerializeField] private Color defaultColor;
        [SerializeField] private Animator animator;

        private bool faded;
        private IWandStatus buffStatus;
        private Waypoint[] waypoints;
        private Vector2Int currentDirection;

        public virtual void Start()
        {
            waypoints = null;
            waypoints = GameObject.FindObjectsOfType<Waypoint>();
            defaultColor = Color.white;
            currentDirection = startDirection;
        }
        
        public override void OnEnable()
        {
            base.OnEnable();
            buffStatus = GameObject.Find("WandStatus").GetComponent<IWandStatus>();
            if (buffStatus.IsBuffActive && !faded) Fade(true);
            GameStatus.OnPlayerDied += StopMoving;
        }
        
        public void OnDestroy()
        {
            GameStatus.OnPlayerDied -= StopMoving;
        }
        
        public void StopMoving()
        {
            animator.enabled = false;
            SetNewDirection(Vector2Int.zero);
        }
        
        private void SetNewDirection(Vector2Int dir)
        {
            currentDirection = dir;
            Debug.Log($"New direction is X:{dir.x} and Y:{dir.y}");
            if (dir.x >= 0)
            {
                _spriteContainer.transform.rotation = Quaternion.identity;
            }
            else if (dir.x < 0)
            {
                _spriteContainer.transform.rotation = Quaternion.identity;
                _spriteContainer.transform.Rotate(new Vector2(0,180));
            }
        }

        public virtual void FixedUpdate()
        {
            if (currentDirection.y != 0)
            {
                Move(new Vector2Int(0, currentDirection.y));
                return;
            }
            Move(new Vector2Int(currentDirection.x, 0));
        }

        public virtual void Move(Vector2Int dir)
        {
            for (int i = 1; i <= ppf; i++)
            {
                Vector2 lastPos = transform.position;
                transform.position += (Vector3)(Vector2)dir;
                var curPos = Vector2Int.RoundToInt(transform.position);
                var way = IsWaypoint(curPos);
                if (way != null)
                {
                    DoWaypointSwitch(way);
                    break;
                }
            }
        }

        private void DoWaypointSwitch(Waypoint way)
        {
            if (!way.gameObject.activeSelf) return;
            if (way.isAbsolute)
            {
                if (way.left) SetNewDirection(Vector2Int.left);
                else if (way.right) SetNewDirection(Vector2Int.right);
                else if (way.up) SetNewDirection(new Vector2Int(currentDirection.x, 1));
                else if (way.down) SetNewDirection(new Vector2Int(currentDirection.x, -1));
                return;
            }
            else
            {
                if (mobType != MobType.Spider && mobType != MobType.FastSpider) return;
                var isVertical = way.up || way.down;
                if (currentDirection.y == 0 && isVertical)
                {
                    if (UnityEngine.Random.Range(1, 101) < 50) return;
                    if (way.up && !way.down)
                    {
                        SetNewDirection(new Vector2Int(currentDirection.x, 1));
                    }
                    else if (!way.up && way.down)
                    {
                        SetNewDirection(new Vector2Int(currentDirection.x, -1));
                    }
                    else if (way.up && way.down)
                    {
                        if (UnityEngine.Random.Range(1, 101) < 50)
                        {
                            SetNewDirection(new Vector2Int(currentDirection.x, 1));
                        }
                        else
                        {
                            SetNewDirection(new Vector2Int(currentDirection.x, -1));
                        }
                    }
                }
                else if (currentDirection.y != 0)
                {
                    Debug.Log("Trying to choose horizontal...");
                    if (way.left && currentDirection.x < 0)
                    {
                        Debug.Log("Setting left.");
                        SetNewDirection(Vector2Int.left);
                        return;
                    }
                    if (way.right && currentDirection.x > 0)
                    {
                        Debug.Log("Setting right.");
                        SetNewDirection(Vector2Int.right);
                        return;
                    }
                }
            }
        }

        private Waypoint IsWaypoint(Vector2Int curPos)
        {
            if (waypoints.Length == 0 || waypoints == null) return null;
            foreach (var waypoint in waypoints)
            {
                if (waypoint == null) return null;
                if ((Vector3)(Vector2)curPos == waypoint.transform.position) return waypoint;
            }
            return null;
        }

        public void Kill()
        {
            SetDirty();
        }

        public override void SetDirty()
        {
            IsDirty = true;
            tracker.SetObjectDirty(gameObject, startPosition);
            gameObject.SetActive(false);
        }

        public override void SetClean()
        {
            IsDirty = false;
            currentDirection = startDirection;
        }

        internal void Fade(bool fading)
        {
            faded = fading;
            var state = animator.GetCurrentAnimatorStateInfo(0);
            if (faded)
            {
                animator.SetFloat("frame", state.normalizedTime);
                animator.SetBool("faded", true);
            }
            else
            {
                animator.SetFloat("frame", state.normalizedTime);
                animator.SetBool("faded", false);
            }
        }
    }
}
