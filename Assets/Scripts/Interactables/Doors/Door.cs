using UnityEngine;
using Com.Technitaur.GreenBean.Core;

namespace Com.Technitaur.GreenBean.Interactables
{
    public class Door : Trackable
    {
        public ItemType UnlockedBy { get { return unlockedBy; } }

        [Header("Door")]
        [SerializeField] private ItemType unlockedBy = ItemType.None;
        
        public override void SetDirty()
        {
            base.SetDirty();
            var waypoints = gameObject.GetComponentsInChildren<Waypoint>();
            foreach (var waypoint in waypoints)
            {
                waypoint.gameObject.SetActive(false);
            }
        }
    }
}
