using UnityEngine;

namespace Com.Technitaur.GreenBean.Inventory
{
    public class Slot : MonoBehaviour
    {
        public Item slotItem;
        
        [SerializeField] private SpriteRenderer rend;
        
        public void Clear()
        {
            rend.sprite = null;
        }
        
    }
}
