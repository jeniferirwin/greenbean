using UnityEngine;

namespace GreenBean.EnvironmentData
{
    public class WallChecker : MonoBehaviour
    {
        public bool blocked;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            blocked = true;
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            blocked = true;
        }
        
        private void OnCollisionExit2D(Collision2D collision)
        {
            blocked = false;    
        }
    }
}