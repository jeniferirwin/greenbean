using UnityEngine;

namespace GreenBean.EnvironmentData
{
    public class WallChecker : MonoBehaviour
    {
        public bool blocked;

        private void OnTriggerEnter2D()
        {
            blocked = true;
        }

        private void OnTriggerStay2D()
        {
            blocked = true;
        }
        
        private void OnTriggerExit2D()
        {
            blocked = false;    
        }
    }
}