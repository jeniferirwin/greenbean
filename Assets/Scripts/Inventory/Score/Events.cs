using UnityEngine;
using Com.Technitaur.GreenBean.Core;

namespace Com.Technitaur.GreenBean.Inventory
{
    public static class Events
    {
        public delegate void ItemPickedUp(ItemType itemType, int worth);
        public static event ItemPickedUp OnItemPickedUp;

        public delegate void ItemConsumed(ItemType itemType, int worth);
        public static event ItemConsumed OnItemConsumed;
        
        public static void PickupItem(ItemType itemType, int worth)
        {
            OnItemPickedUp?.Invoke(itemType, worth);
        }
        
        public static void ConsumeItem(ItemType itemType, int worth)
        {
            OnItemConsumed?.Invoke(itemType, worth);
        }
    }
}
