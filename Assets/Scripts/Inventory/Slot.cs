using UnityEngine;
using Com.Technitaur.GreenBean.Core;

namespace Com.Technitaur.GreenBean.Inventory
{
    public class Slot : MonoBehaviour
    {
        public int num;
        public Inventory inventory;
        public SpriteRenderer spriteRenderer;
        public SpriteDatabase database;

        private void Start()
        {
            inventory.UpdateSlots += UpdateSlot;
            spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        }

        private void UpdateSlot()
        {
            Sprite[] newItem;
            if (inventory.items.Count <= num) return;
            switch (inventory.items[num])
            {
                case ItemType.RedKey:
                    newItem = database.redKey;
                    break;
                case ItemType.VioletKey:
                    newItem = database.violetKey;
                    break;
                case ItemType.CyanKey:
                    newItem = database.cyanKey;
                    break;
                case ItemType.Wand:
                    newItem = database.wand;
                    break;
                case ItemType.Torch:
                    newItem = database.torch;
                    break;
                case ItemType.Coin:
                    newItem = database.coin;
                    break;
                case ItemType.Sword:
                    newItem = database.sword;
                    break;
                default:
                    newItem = database.none;
                    break;
            }
            spriteRenderer.sprite = newItem[0];
        }
    }
}
