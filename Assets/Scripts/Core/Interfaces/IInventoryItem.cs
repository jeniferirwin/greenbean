using UnityEngine;

namespace Com.Technitaur.GreenBean.Core
{
    public interface IInventoryItem
    {
        string Name { get; }
        ItemType ItemType { get; }
        int PickupWorth { get; }   // score addition when picked up
        int ConsumeWorth { get; }  // score addition when consumed
        Sprite Sprite { get; }
        
        void OnPickup();
    }
}
