using UnityEngine;

namespace Com.Technitaur.GreenBean.Interactables
{
    public class DoorOpener : MonoBehaviour
    {
        [SerializeField] private BoxCollider2D openerCollider;
        
        private void OnTriggerEnter2D(Collider2D collider)
        {
            Door door;
            if (!collider.gameObject.TryGetComponent<Door>(out door)) return;
        }
    }
}
