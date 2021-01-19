using UnityEngine;
using Com.Technitaur.GreenBean.Core;

namespace Com.Technitaur.GreenBean.Interactables
{
    public class DoorOpener : MonoBehaviour
    {
        [SerializeField] private GameObject playerInventoryObject = null;
        private IInventory inv;
        
        private void Start()
        {
            playerInventoryObject = GameObject.Find("Inventory");
            if (!playerInventoryObject.TryGetComponent<IInventory>(out inv))
            {
                throw new System.Exception("Player inventory was not found.");
            }
        }
        private void OnTriggerEnter2D(Collider2D collider)
        {
            Door door;
            door = collider.gameObject.GetComponentInParent<Door>();
            if (door == null) return;
            if (TryConsumeKey(door.UnlockedBy))
            {
                door.SetDirty();
            }
        }

        private bool TryConsumeKey(ItemType unlockedBy)
        {
            if (inv.Consume(unlockedBy)) return true;
            else return false;
        }
    }
}
