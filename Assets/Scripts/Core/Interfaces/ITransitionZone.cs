using UnityEngine;

namespace Com.Technitaur.GreenBean.Core
{
    public interface ITransitionZone
    {
        Direction Direction { get; }
        Room Room { get; }
    }
}