using UnityEngine;

namespace Com.Technitaur.GreenBean.Interactables
{
    public class Door : Trackable
    {
        public Inventory.ItemType UnlockedBy { get { return unlockedBy; } }

        [Header("Door")]
        [SerializeField] private Inventory.ItemType unlockedBy;
    }
}
