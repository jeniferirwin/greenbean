using UnityEngine;
using Com.Technitaur.GreenBean.Core;

namespace Com.Technitaur.GreenBean.Interactables
{
    public class Wand : Trackable, IInventoryItem
    {
        public string Name { get { return itemName; } }
        public ItemType ItemType { get { return itemType; }}
        public int PickupWorth { get { return pickupWorth; }}
        public int ConsumeWorth { get { return consumeWorth; }}
        public Sprite Sprite { get { return CleanSprite; }}

        [Header("Key")]
        [SerializeField] private string itemName = "Wand";
        [SerializeField] private ItemType itemType = ItemType.Wand;
        [SerializeField] private int pickupWorth = 100;
        [SerializeField] private int consumeWorth = 0;

        public void OnPickup()
        {
            SetDirty();
            var status = GameObject.Find("WandStatus").GetComponent<IWandStatus>();
            status.ActivateBuff();
        }

        public void OnConsume()
        {
            // TODO: figure out if I even need this function
        }
    }
}
