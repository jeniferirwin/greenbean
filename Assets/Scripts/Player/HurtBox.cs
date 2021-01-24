using UnityEngine;
using Com.Technitaur.GreenBean.Core;

namespace Com.Technitaur.GreenBean.Player
{
    public class HurtBox : MonoBehaviour
    {
        [SerializeField] private Controller player;
        [SerializeField] private IInventory _inv;
        [SerializeField] private IWandStatus _wandBuff;

        private void Start()
        {
            _inv = GameObject.FindGameObjectWithTag("Inventory").GetComponent<IInventory>();
            _wandBuff = GameObject.Find("WandStatus").GetComponent<IWandStatus>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Mob"))
            {
                if (_wandBuff.IsBuffActive) return;
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
