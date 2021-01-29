using UnityEngine;
using System.Collections.Generic;
using Com.Technitaur.GreenBean.Core;

namespace Com.Technitaur.GreenBean.Inventory
{
    public class Slot : MonoBehaviour
    {
        public IInventoryItem slotItem;
        
        [SerializeField] private int index = 0;
        [SerializeField] private SpriteRenderer rend = null;
        [SerializeField] private Animator anim = null;
        
        public void Start()
        {
            PlayerInventory.OnInventoryUpdate += UpdateSlot;
        }
        
        public void OnDestroy()
        {
            PlayerInventory.OnInventoryUpdate -= UpdateSlot;
        }

        public void Clear()
        {
            if (anim == null) return;
            anim.enabled = false;
            rend.sprite = null;
        }
        
        public void UpdateSlot(List<IInventoryItem> items)
        {
            if (index <= items.Count - 1)
            {
                if (items[index].ItemType == ItemType.Torch) anim.enabled = true;
                else
                {
                    anim.enabled = false;
                    rend.sprite = items[index].Sprite;
                }
            }
            else
            {
                Clear();
            }
        }
        
    }
}
