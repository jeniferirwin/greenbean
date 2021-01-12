using UnityEngine;

namespace Com.Technitaur.GreenBean.Core
{
    public interface ITracked
    {
        bool IsDirty { get; set; }
        int TrackedID { get; set; }
        void SetDirty();
        void SetClean();
        GameObject GetGameObject();
    }
}
