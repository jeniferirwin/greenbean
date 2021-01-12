using UnityEngine;

namespace Com.Technitaur.GreenBean.Interactables
{
    public class Key : Trackable, ITakeable
    {
        public Inventory.ItemType Color { get { return keyType; } }
        public int Worth { get { return worth; } }

        [Header("Key")]
        [SerializeField] private Inventory.ItemType keyType;
        [SerializeField] private int worth;
        
        public void OnTake() {}
    }
}
