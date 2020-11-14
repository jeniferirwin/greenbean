using UnityEngine;

namespace GreenBean.Helpers
{
    public class JumpData
    {
        public GameObject player;
        public bool peaked;
        public Vector2 direction;
        public float leaveGroundTimer = 0.25f;
        
        private Vector2 startPosition;
        private float maxYPoint;

        public JumpData(GameObject thisPlayer, float jumpHeight, Vector2 initDirection)
        {
            player = thisPlayer;
            maxYPoint = player.transform.position.y + jumpHeight;
            peaked = false;
            direction = initDirection;
            Debug.Log("Jump direction: " + direction);
        }
        
        public void Update()
        {
            leaveGroundTimer -= Time.deltaTime;
            if (player.transform.position.y >= maxYPoint)
            {
                peaked = true;
            }
        }
    }
}