using UnityEngine;
using Com.Technitaur.GreenBean.Core;

namespace Com.Technitaur.GreenBean.Interactables
{
    public class Key : Trackable, ITakeable
    {
        public ItemType Color { get { return keyType; } }
        public int Worth { get { return worth; } }

        [Header("Key")]
        [SerializeField] private ItemType keyType = ItemType.None;
        [SerializeField] private int worth = 0;
    }
}
