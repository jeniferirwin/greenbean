using UnityEngine;
using Com.Technitaur.GreenBean.Core;

namespace Com.Technitaur.GreenBean.Interactables
{
    public class Coin : Trackable, IInventoryItem
    {
        public string Name { get { return itemName; } }
        public ItemType ItemType { get { return itemType; }}
        public int PickupWorth { get { return pickupWorth; }}
        public int ConsumeWorth { get { return consumeWorth; }}
        public Sprite Sprite { get { return CleanSprite; }}

        [Header("Coin")]
        [SerializeField] private string itemName = "Coin";
        [SerializeField] private ItemType itemType = ItemType.Coin;
        [SerializeField] private int pickupWorth = 1000;
        [SerializeField] private int consumeWorth = 0;

        public void OnPickup()
        {
            SetDirty();
        }
    }
}
