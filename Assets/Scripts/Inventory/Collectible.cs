using UnityEngine;
using Com.Technitaur.GreenBean.Core;

namespace Com.Technitaur.GreenBean.Inventory
{
    public class Collectible : MonoBehaviour, ICollectible
    {
        public ItemType Item { get { return itemType; } }
        [SerializeField] private ItemType itemType;
        public bool touched;

        public void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.CompareTag("Player") && !touched)
            {
                GameObject invObj = GameObject.FindGameObjectWithTag("Inventory");
                Inventory inv = invObj.GetComponent<Inventory>();
                if (inv.HasSpace())
                {
                    inv.AddItem(itemType);
                    touched = true;
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
