using UnityEngine;

namespace Com.Technitaur.GreenBean.Core
{
    public interface ISensor
    {
        bool AtLadder { get; }
        bool AtRope { get; }
        bool AtSolid { get; }
        bool AtSemiSolid { get; }
        bool AtHazard { get; }
        bool AtCollectible { get; }
        bool AtPole { get; }
        bool AtClosedDoor { get; }
        bool AtLeftBelt { get; }
        bool AtRightBelt { get; }
        
        void SensorUpdate();
    }
}
