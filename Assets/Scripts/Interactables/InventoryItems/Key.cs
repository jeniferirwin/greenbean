using UnityEngine;
using Com.Technitaur.GreenBean.Core;

namespace Com.Technitaur.GreenBean.Interactables
{
    public class Key : Trackable, IInventoryItem
    {
        public string Name { get { return itemName; } }
        public ItemType ItemType { get { return itemType; }}
        public int PickupWorth { get { return pickupWorth; }}
        public int ConsumeWorth { get { return consumeWorth; }}
        public Sprite Sprite { get { return CleanSprite; }}

        [Header("Key")]
        [SerializeField] private string itemName = "Key";
        [SerializeField] private ItemType itemType = ItemType.None;
        [SerializeField] private int pickupWorth = 50;
        [SerializeField] private int consumeWorth = 300;

        public void OnPickup()
        {
            SetDirty();
        }
    }
}
