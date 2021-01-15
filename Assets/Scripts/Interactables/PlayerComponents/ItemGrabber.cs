using UnityEngine;
using Com.Technitaur.GreenBean.Core;

namespace Com.Technitaur.GreenBean.Interactables
{
    public class ItemGrabber : MonoBehaviour
    {
        [SerializeField] private BoxCollider2D grabber;
        
        private IInventory _inventory;
        
        private void Start()
        {
            _inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<IInventory>();
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (_inventory.IsFull) return;
            
            if (collider.gameObject.TryGetComponent<ITakeable>(out ITakeable takeable))
            {
            }
        }
    }
}
