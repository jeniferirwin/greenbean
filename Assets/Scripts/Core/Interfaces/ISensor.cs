using UnityEngine;

namespace Com.Technitaur.GreenBean.Core
{
    public interface ISensor
    {
        bool AtLadder { get; }
        bool AtRope { get; }
        bool AtSolid { get; }
        bool AtSemisolid { get; }
        bool AtPole { get; }
        bool AtLeftBelt { get; }
        bool AtRightBelt { get; }
        
        void SensorUpdate();
    }
}
