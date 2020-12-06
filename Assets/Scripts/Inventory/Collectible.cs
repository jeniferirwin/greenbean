using UnityEngine;
using Com.Technitaur.GreenBean.Core;

namespace Com.Technitaur.GreenBean.Inventory
{
    public class Collectible : MonoBehaviour, ICollectible
    {
        public ItemType Item { get { return itemType; } }
        public int VanishTimer { get { return vanishTime; } }
        public int Score { get { return score; } }
        [SerializeField] private ItemType itemType;
        [SerializeField] private int vanishTime;
        [SerializeField] private int score;
        public bool touched;

        public void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.CompareTag("Player") && !touched)
            {
                GameObject invObj = GameObject.FindGameObjectWithTag("Inventory");
                Inventory inv = invObj.GetComponent<Inventory>();
                if (inv.HasSpace())
                {
                    if (vanishTime != 0) inv.AddItem(itemType);
                    touched = true;
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
