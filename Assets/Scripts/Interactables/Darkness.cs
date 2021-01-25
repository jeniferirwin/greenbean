using UnityEngine;
using Com.Technitaur.GreenBean.Core;

namespace Com.Technitaur.GreenBean.Interactables
{
    public class Darkness : MonoBehaviour
    {
        private IInventory _inv = null;

        public void Start()
        {
            _inv = GameObject.Find("Inventory").GetComponent<IInventory>();
            if (_inv.HasItem(ItemType.Torch))
            {
                OnTorchPickup();
            }
        }
        
        public void OnTorchPickup()
        {
            gameObject.SetActive(false);
        }
    }
}
