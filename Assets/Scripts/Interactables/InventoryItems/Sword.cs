using UnityEngine;
using Com.Technitaur.GreenBean.Core;

namespace Com.Technitaur.GreenBean.Interactables
{
    public class Sword : Trackable, IInventoryItem
    {
        public string Name { get { return itemName; } }
        public ItemType ItemType { get { return itemType; }}
        public int PickupWorth { get { return pickupWorth; }}
        public int ConsumeWorth { get { return consumeWorth; }}
        public Sprite Sprite { get { return CleanSprite; }}

        [Header("Key")]
        [SerializeField] private string itemName = "Sword";
        [SerializeField] private ItemType itemType = ItemType.Sword;
        [SerializeField] private int pickupWorth = 100;
        [SerializeField] private int consumeWorth = 0;

        public void OnPickup()
        {
            SetDirty();
        }
    }
}
