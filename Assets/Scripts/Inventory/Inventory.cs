using UnityEngine;

namespace Com.Technitaur.GreenBean.Inventory
{
    public static class Inventory
    {
        public static Item[] slots;
        public enum Item { None, RedKey, CyanKey, VioletKey, Torch, Sword, Wand, Coin }
        
        public static Item GetItem(int slot)
        {
            return slots[slot];
        }

        public static bool HasItem(Item item)
        {
            foreach (Item slot in slots)
            {
                if (slot == item) return true;
            }
            return false;
        }
        
        public static bool RemoveItem(Item item)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i] == item)
                {
                    slots[i] = Item.None;
                    ShiftItems();
                    return true;
                }
            }
            return false;
        }
        
        public static void ShiftItems()
        {
            Item[] newSlots = new Item[slots.Length - 1];
            for (int i = 0; i < newSlots.Length; i++)
            {
                newSlots[i] = Item.None;
            }

            int counter = 0;
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i] != Item.None)
                {
                    newSlots[counter] = slots[i];
                    counter++;
                }
            }
            slots = newSlots;
        }
        
        public static bool AddItem(Item item)
        {
            if (!HasEmptySlot()) return false;
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i] == Item.None)
                {
                    slots[i] = item;
                    return true;
                }
            }
            return false;
        }
        
        public static bool HasEmptySlot()
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i] == Item.None) return true;
            }
            return false;
        }
    }
}
