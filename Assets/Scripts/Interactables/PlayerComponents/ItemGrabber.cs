using UnityEngine;
using Com.Technitaur.GreenBean.Core;
using System.Collections;

namespace Com.Technitaur.GreenBean.Interactables
{
    public class ItemGrabber : MonoBehaviour
    {
        [SerializeField] private BoxCollider2D grabber;
        
        private IInventory _inventory;
        private Coroutine wandDecay;
        
        private void Start()
        {
            _inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<IInventory>();
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (_inventory.IsFull) return;
            IInventoryItem item = collider.gameObject.GetComponentInParent<IInventoryItem>();
            
            if (item != null)
            {
                _inventory.Add(item);
                item.OnPickup();
                if (item.ItemType == ItemType.Coin)
                {
                    _inventory.Consume(ItemType.Coin);
                }
                if (item.ItemType == ItemType.Wand)
                {
                    wandDecay = StartCoroutine(WandDecay());
                }
            }
        }
        
        private IEnumerator WandDecay()
        {
            yield return new WaitForSeconds(4);
            _inventory.Consume(ItemType.Wand);
        }
    }
}
