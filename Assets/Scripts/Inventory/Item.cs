using UnityEngine;
using Com.Technitaur.GreenBean.Core;

namespace Com.Technitaur.GreenBean.Inventory
{
    [CreateAssetMenu(fileName = "InventoryItem", menuName = "Inventory Item", order = 1)]
    public class Item : ScriptableObject
    {
        public ItemType type;
        public Sprite Sprite;
        public int Worth; // this is when consumed, not when picked up
    }
}
