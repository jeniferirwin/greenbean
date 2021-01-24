using UnityEngine;
using Com.Technitaur.GreenBean.Core;

namespace Com.Technitaur.GreenBean.Player
{
    public class HurtBox : MonoBehaviour
    {
        [SerializeField] private Controller player;
        [SerializeField] private IInventory _inv;

        private void Start()
        {
            _inv = GameObject.FindGameObjectWithTag("Inventory").GetComponent<IInventory>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Mob"))
            {
                var mobInfo = other.GetComponentInParent<IKillableMob>();
                var worth = mobInfo.PointValue;
                var playerInfo = player.GetComponent<IKillable>();
                mobInfo.Kill();
                if (!_inv.Consume(ItemType.Sword, worth))
                {
                    playerInfo.Kill();
                }
            }
        }
    }
}
