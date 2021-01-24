using UnityEngine;

namespace Com.Technitaur.GreenBean.Core
{
    public interface IWandStatus
    {
        bool IsBuffActive { get; }
        void ActivateBuff();
    }
}
