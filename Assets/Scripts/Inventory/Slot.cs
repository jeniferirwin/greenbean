using UnityEngine;
using System.Collections.Generic;
using Com.Technitaur.GreenBean.Core;

namespace Com.Technitaur.GreenBean.Inventory
{
    public class Slot : MonoBehaviour
    {
        public IInventoryItem slotItem;
        
        [SerializeField] private int index = 0;
        [SerializeField] private SpriteRenderer rend = null;
        
        public void Start()
        {
            PlayerInventory.OnInventoryUpdate += UpdateSlot;
        }

        public void Clear()
        {
            rend.sprite = null;
        }
        
        public void UpdateSlot(List<IInventoryItem> items)
        {
            if (index <= items.Count - 1)
            {
                rend.sprite = items[index].Sprite;
            }
            else
            {
                Clear();
            }
        }
        
    }
}
