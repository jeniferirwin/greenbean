using UnityEngine;
using Com.Technitaur.GreenBean.Core;
using System.Collections.Generic;

namespace Com.Technitaur.GreenBean.Inventory
{
    public class PlayerInventory : MonoBehaviour, IInventory
    {
        public delegate void InventoryUpdate(List<IInventoryItem> items);
        public static event InventoryUpdate OnInventoryUpdate;

        public List<IInventoryItem> items = new List<IInventoryItem>();

        public int Count { get { return items.Count; } }

        public bool IsFull
        {
            get
            {
                if (Count >= 5) return true;
                else return false;
            }
        }
        
        public bool Consume(ItemType itemType)
        {
            var idx = FindItemIndex(itemType);
            if (idx == -1) return false; 
            Inventory.Events.ConsumeItem(items[idx].ConsumeWorth);
            items.RemoveAt(idx);
            OnInventoryUpdate?.Invoke(items);
            return true;
        }
        
        public bool Consume(ItemType itemType, int worth)
        {
            var idx = FindItemIndex(itemType);
            if (idx == -1) return false; 
            Inventory.Events.ConsumeItem(worth);
            items.RemoveAt(idx);
            OnInventoryUpdate?.Invoke(items);
            return true;
        }
        
        public int FindItemIndex(ItemType itemType)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].ItemType == itemType)
                {
                    return i;
                }
            }
            return -1;
        }
        
        public bool Add(IInventoryItem item)
        {
            if (IsFull) return false;
            
            Inventory.Events.PickupItem(item.PickupWorth);
            items.Add(item);
            OnInventoryUpdate?.Invoke(items);
            return true;
        }
    }
}
