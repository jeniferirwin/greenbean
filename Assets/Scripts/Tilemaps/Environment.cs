using UnityEngine;
using Com.Technitaur.GreenBean.Core;

namespace Com.Technitaur.GreenBean.Player
{
    public class Environment : MonoBehaviour, IEnvironment
    {
        public GameObject sensorAtFeet;
        public GameObject sensorBelowFeet;
        public GameObject sensorAtLeft;
        public GameObject sensorAtRight;

        private ISensor atFeet;
        private ISensor belowFeet;
        private ISensor atLeft;
        private ISensor atRight;

        public void Start()
        {
            atFeet = sensorAtFeet.GetComponent<ISensor>();
            belowFeet = sensorBelowFeet.GetComponent<ISensor>();
            atLeft = sensorAtLeft.GetComponent<ISensor>();
            atRight = sensorAtRight.GetComponent<ISensor>();
        }

        public bool IsGrounded
        {
            get
            {
                belowFeet.SensorUpdate();
                atFeet.SensorUpdate();
                if (belowFeet.AtSemisolid || belowFeet.AtSolid)
                {
                    if (!atFeet.AtSemisolid) return true;
                }
                return false;
            }
        }
        
        public bool IsOnFire
        {
            get
            {
                atFeet.SensorUpdate();
                belowFeet.SensorUpdate();
                atRight.SensorUpdate();
                atLeft.SensorUpdate();
                if (atFeet.AtHazard || belowFeet.AtHazard || atRight.AtHazard || atLeft.AtHazard) return true;
                return false;
            }
        }
        
        public bool LadderSnapRight
        {
            get
            {
                atFeet.SensorUpdate();
                belowFeet.SensorUpdate();
                if (atFeet.AtLeftLadder || belowFeet.AtLeftLadder) return true;
                return false;
            }
        }

        public bool CanClimbUpLadder
        {
            get
            {
                atFeet.SensorUpdate();
                if (atFeet.AtLeftLadder || atFeet.AtRightLadder || atFeet.AtRightLadderTop || atFeet.AtLeftLadderTop) return true;
                return false;
            }
        }

        public bool CanClimbDownLadder
        {
            get
            {
                belowFeet.SensorUpdate();
                if (belowFeet.AtLeftLadder || belowFeet.AtRightLadder || belowFeet.AtLeftLadderTop || belowFeet.AtRightLadderTop) return true;
                return false;
            }
        }

        public bool CanClimbUpRope
        {
            get
            {
                atFeet.SensorUpdate();
                if (atFeet.AtRope) return true;
                return false;
            }
        }

        public bool CanClimbDownRope
        {
            get
            {
                belowFeet.SensorUpdate();
                if (belowFeet.AtRope) return true;
                return false;
            }
        }

        public bool CanSlide
        {
            get
            {
                belowFeet.SensorUpdate();
                if (belowFeet.AtPole) return true;
                return false;
            }
        }

        public bool CanMoveRight
        {
            get
            {
                atRight.SensorUpdate();
                if (atRight.AtSolid || atRight.AtClosedDoor) return false;
                return true;
            }
        }

        public bool CanMoveLeft
        {
            get
            {
                atLeft.SensorUpdate();
                if (atLeft.AtSolid || atLeft.AtClosedDoor) return false;
                return true;
            }
        }

        public bool IsOnLeftBelt
        {
            get
            {
                belowFeet.SensorUpdate();
                if (belowFeet.AtLeftBelt) return true;
                return false;
            }
        }

        public bool IsOnRightBelt
        {
            get
            {
                belowFeet.SensorUpdate();
                if (belowFeet.AtRightBelt) return true;
                return false;
            }
        }
        
        public Vector2Int HorizontalSnap(Vector2Int pos, bool right)
        {
            if (right)
            {
                pos.x++;
                while (pos.x % 8 != 0)
                {
                    pos.x++;
                }
            }
            else
            {
                while (pos.x % 8 != 0)
                {
                    pos.x--;
                }
            }
            return pos;
        }
        
        public Vector2Int CenterXYSnap(Vector2Int pos)
        {
            return Vector2Int.RoundToInt(atFeet.CurrentTileCenter());
        }
        
        public Vector2Int CenterXSnap(Vector2Int pos)
        {
            var tileX = atFeet.CurrentTileCenter().x;
            return Vector2Int.RoundToInt(new Vector2(tileX, pos.y));
        }
    }
}
