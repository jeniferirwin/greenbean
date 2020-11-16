using UnityEngine;

namespace GreenBean.EnvironmentData
{
    public class RopeChecker : MonoBehaviour
    {
        public bool canClimb;

        private void Start()
        {
            canClimb = false;
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            canClimb = true;
        }

        private void OnTriggerStay2D(Collider2D collider)
        {
            canClimb = true;
        }

        private void OnTriggerExit2D(Collider2D collider)
        {
            canClimb = false;
        }
    }
}