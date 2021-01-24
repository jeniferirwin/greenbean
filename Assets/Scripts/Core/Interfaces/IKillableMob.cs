using UnityEngine;

namespace Com.Technitaur.GreenBean.Core
{
    public interface IKillableMob : IKillable
    {
        int PointValue { get; }
        bool CanDieToSword { get; }
    }
}