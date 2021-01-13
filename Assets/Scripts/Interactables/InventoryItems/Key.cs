using UnityEngine;

namespace Com.Technitaur.GreenBean.Interactables
{
    public class Key : Trackable
    {
        public Inventory.ItemType Color { get { return keyType; } }
        public int Worth { get { return worth; } }

        [Header("Key")]
        [SerializeField] private Inventory.ItemType keyType = Inventory.ItemType.None;
        [SerializeField] private int worth = 0;
    }
}
