using UnityEngine;

namespace Com.Technitaur.GreenBean.Core
{
    public interface ISensor
    {
        bool AtLeftLadderTop { get; } 
        bool AtRightLadderTop { get; } 
        bool AtLeftLadder { get; }
        bool AtRightLadder { get; }
        bool AtRope { get; }
        bool AtSolid { get; }
        bool AtSemisolid { get; }
        bool AtPole { get; }
        bool AtLeftBelt { get; }
        bool AtRightBelt { get; }
        bool AtClosedDoor { get; }
        
        void SensorUpdate();
    }
}
