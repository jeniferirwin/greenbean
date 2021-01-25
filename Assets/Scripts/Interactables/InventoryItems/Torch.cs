using UnityEngine;
using Com.Technitaur.GreenBean.Core;

namespace Com.Technitaur.GreenBean.Interactables
{
    public class Torch : Trackable, IInventoryItem
    {
        public string Name { get { return itemName; } }
        public ItemType ItemType { get { return itemType; }}
        public int PickupWorth { get { return pickupWorth; }}
        public int ConsumeWorth { get { return consumeWorth; }}
        public Sprite Sprite { get { return CleanSprite; }}

        [Header("Torch")]
        [SerializeField] private string itemName = "Torch";
        [SerializeField] private ItemType itemType = ItemType.Torch;
        [SerializeField] private int pickupWorth = 100;
        [SerializeField] private int consumeWorth = 0;

        public void OnPickup() => SetDirty();
        public void OnConsume() {}
        
        public override void SetDirty()
        {
            IsDirty = true;
            objCollider.gameObject.SetActive(false);
            rend.gameObject.SetActive(false);
            tracker.SetObjectDirty(gameObject, startPosition);
        }
        
        public override void SetClean()
        {
            IsDirty = false;
            objCollider.gameObject.SetActive(true);
            rend.gameObject.SetActive(true);
        }
    }
}
