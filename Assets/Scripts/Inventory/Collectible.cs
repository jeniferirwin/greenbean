using UnityEngine;
using Com.Technitaur.GreenBean.Core;

namespace Com.Technitaur.GreenBean.Inventory
{
    public class Collectible : MonoBehaviour, ICollectible
    {
        public ItemType ItemType { get; }
        [SerializeField] private ItemType itemType;

        public void OnTriggerStay2D(Collider2D collider)
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                Debug.Log("Player has touched key");
            }
        }
    }
}
