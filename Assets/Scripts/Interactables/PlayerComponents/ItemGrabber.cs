using UnityEngine;

namespace Com.Technitaur.GreenBean.Interactables
{
    public class ItemGrabber : MonoBehaviour
    {
        [SerializeField] private BoxCollider2D grabber;
        
        private void OnTriggerEnter2D(Collider2D collider)
        {
        }
    }
}
