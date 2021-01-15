using UnityEngine;

namespace Com.Technitaur.GreenBean.Core
{
    public interface IInventory
    {
        bool IsFull { get; }
        int Count { get; }
        bool Add(IInventoryItem item);
        bool Consume(ItemType itemType);
    }
}
