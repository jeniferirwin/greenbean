using UnityEngine;
using Com.Technitaur.GreenBean.Core;

namespace Tests
{
    public class FakeInventoryItem : IInventoryItem
    {
        public string Name { get; private set; }
        public ItemType ItemType { get; private set; }
        public int PickupWorth { get; private set; }
        public int ConsumeWorth { get; private set; }

        public FakeInventoryItem(string name, ItemType itemType, int pickupWorth, int consumeWorth)
        {
            Name = name;
            ItemType = itemType;
            PickupWorth = pickupWorth;
            ConsumeWorth = consumeWorth;
        }

        public void OnConsume()
        {
            Debug.Log($"Fake {Name} was consumed.");
        }

        public void OnPickup()
        {
            Debug.Log($"Fake {Name} was picked up.");
        }
    }
}
