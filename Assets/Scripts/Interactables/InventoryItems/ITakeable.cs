using UnityEngine;

namespace Com.Technitaur.GreenBean.Interactables
{
    public interface ITakeable
    {
        int Worth { get; }
        void OnTake();
    }
}
