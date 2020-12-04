using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Com.Technitaur.GreenBean.Inventory
{
    public class Slot : MonoBehaviour
    {
        public int number;
        public SpriteRenderer spriteRenderer;
        public SpriteTable table;
        public Dictionary<Inventory.Item,Sprite[]> display = new Dictionary<Inventory.Item,Sprite[]>();

        public void Start()
        {
            spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            BuildDictionary();
        }
        
        public void BuildDictionary()
        {
            display[Inventory.Item.Coin] = table.coin;
            display[Inventory.Item.Sword] = table.sword;
            display[Inventory.Item.RedKey] = table.redKey;
            display[Inventory.Item.CyanKey] = table.cyanKey;
            display[Inventory.Item.VioletKey] = table.violetkey;
            display[Inventory.Item.Wand] = table.wand;
            display[Inventory.Item.Torch] = table.torch;
        }

        public void Update()
        {
            spriteRenderer.sprite = display[Inventory.GetItem(number)][0];
        }
    }
}
