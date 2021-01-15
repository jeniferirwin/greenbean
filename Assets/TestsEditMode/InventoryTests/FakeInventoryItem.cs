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
        public Sprite Sprite { get; private set; }

        public FakeInventoryItem(string name, ItemType itemType, int pickupWorth, int consumeWorth)
        {
            Name = name;
            ItemType = itemType;
            PickupWorth = pickupWorth;
            ConsumeWorth = consumeWorth;
            Sprite = FakeSprite();
        }
        
        private Sprite FakeSprite()
        {
            var pivot = Vector2.zero;
            var rect = new Rect(Vector2.zero, new Vector2(0.1f, 0.1f));
            var tex = Texture2D.blackTexture;
            var newSprite = Sprite.Create(tex,rect,pivot,1,0);
            return newSprite;
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
