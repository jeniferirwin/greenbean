using UnityEngine;
using Com.Technitaur.GreenBean.Core;

namespace Com.Technitaur.GreenBean.Player
{
    public class Environment : MonoBehaviour
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
                Debug.Log("GroundCheck");
                if (belowFeet.AtSemiSolid || belowFeet.AtSolid)
                {
                    Debug.Log("IsGrounded");
                    return true;
                }
                return false;
            }
        }

        public bool CanClimbUpLadder
        {
            get
            {
                atFeet.SensorUpdate();
                if (atFeet.AtLadder) return true;
                return false;
            }
        }

        public bool CanClimbDownLadder
        {
            get
            {
                belowFeet.SensorUpdate();
                if (belowFeet.AtLadder) return true;
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
                atFeet.SensorUpdate();
                belowFeet.SensorUpdate();
                if (atFeet.AtPole || belowFeet.AtPole) return true;
                return false;
            }
        }

        public bool CanMoveRight
        {
            get
            {
                atRight.SensorUpdate();
                if (atRight.AtSolid) return false;
                return true;
            }
        }

        public bool CanMoveLeft
        {
            get
            {
                atLeft.SensorUpdate();
                if (atLeft.AtSolid) return false;
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
    }
}
