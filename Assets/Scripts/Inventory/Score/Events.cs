using UnityEngine;

namespace Com.Technitaur.GreenBean.Inventory
{
    public static class Events
    {
        public delegate void ItemPickedUp(int worth);
        public static event ItemPickedUp OnItemPickedUp;

        public delegate void ItemConsumed(int worth);
        public static event ItemConsumed OnItemConsumed;
        
        public static void PickupItem(int worth)
        {
            OnItemPickedUp?.Invoke(worth);
        }
        
        public static void ConsumeItem(int worth)
        {
            OnItemConsumed?.Invoke(worth);
        }
    }
}
