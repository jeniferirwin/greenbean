using UnityEngine;
using System.Collections.Generic;
using Com.Technitaur.GreenBean.Core;

namespace Com.Technitaur.GreenBean.Inventory
{
    public class Inventory : MonoBehaviour
    {
        public delegate void UpdateSlotsEvent();
        public event UpdateSlotsEvent UpdateSlots;

        public List<ItemType> items = new List<ItemType>();

        public void Start()
        {
        }

        public bool AddItem(ItemType item)
        {
            if (HasSpace())
            {
                items.Add(item);
                UpdateSlots?.Invoke();
                return true;
            }
            return false;
        }

        public bool HasSpace()
        {
            if (items.Count < 5) return true;
            else return false;
        }

        public bool HasItem(ItemType item)
        {
            if (items.IndexOf(item) != -1) return true;
            return false;
        }

        public bool RemoveItem(ItemType item)
        {
            int pos = items.IndexOf(item);
            if (pos != -1)
            {
                items.RemoveAt(pos);
                UpdateSlots?.Invoke();
                return true;
            }
            return false;
        }
    }
}
