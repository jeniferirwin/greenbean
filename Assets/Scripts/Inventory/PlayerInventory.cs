﻿using UnityEngine;
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
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].ItemType == itemType)
                {
                    Inventory.Events.ConsumeItem(items[i].ConsumeWorth);
                    items.RemoveAt(i);
                    OnInventoryUpdate?.Invoke(items);
                    return true;
                }
            }
            return false;
        }
        
        public bool Add(IInventoryItem item)
        {
            if (IsFull) return false;
            
            Inventory.Events.PickupItem(item.PickupWorth);
            Debug.Log($"Adding {item.PickupWorth} to score.");
            items.Add(item);
            OnInventoryUpdate?.Invoke(items);
            return true;
        }
    }
}