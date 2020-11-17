using UnityEngine;
using GreenBean.Player;

namespace GreenBean.Ladders
{
    public class LadderZone : MonoBehaviour
    {
        public bool isTop;

        private void OnTriggerEnter2D(Collider2D collider)
        {
            SetClimbable(collider, true);
        }

        private void OnTriggerStay2D(Collider2D collider)
        {
            SetClimbable(collider, true);
        }

        private void OnTriggerExit2D(Collider2D collider)
        {
            SetClimbable(collider, false);
        }

        private void SetClimbable(Collider2D collider, bool isActive)
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                Movement playerInfo = collider.gameObject.GetComponent<Movement>();
                playerInfo.canClimbLadder = isActive;
            }
            else if (collider.gameObject.CompareTag("Spider"))
            {
                // spider code here
            }
        }
    }
}