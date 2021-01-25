using UnityEngine;

namespace Com.Technitaur.GreenBean.Core
{
    public interface IInventory
    {
        bool IsFull { get; }
        int Count { get; }
        bool Add(IInventoryItem item);
        bool Consume(ItemType itemType);
        bool Consume(ItemType itemType, int worth);
        bool HasItem(ItemType itemType);
    }
}
