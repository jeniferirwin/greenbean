using UnityEngine;

namespace GreenBean.EnvironmentData
{
    public class GroundChecker : MonoBehaviour
    {
        public bool isGrounded;
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            isGrounded = true;
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            isGrounded = true;
        }
        
        private void OnCollisionExit2D(Collision2D collision)
        {
            isGrounded = false;    
        }
    }
}