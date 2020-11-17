using UnityEngine;

namespace GreenBean.EnvironmentData
{
    public class GroundChecker : MonoBehaviour
    {
        public bool isGrounded;
        public float yPoint;
        
        private void OnTriggerEnter2D(Collider2D collider)
        {
            isGrounded = true;
            yPoint = collider.ClosestPoint(transform.position).y;
        }

        private void OnTriggerStay2D(Collider2D collider)
        {
            isGrounded = true;
            yPoint = collider.ClosestPoint(transform.position).y;
        }
        
        private void OnTriggerExit2D(Collider2D collider)
        {
            isGrounded = false;    
        }
    }
}