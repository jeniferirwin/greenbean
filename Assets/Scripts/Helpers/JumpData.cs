using UnityEngine;

namespace GreenBean.Helpers
{
    public class JumpData
    {
        public GameObject player;
        public Vector2 direction;
        public Vector2[] velocityPerFixedUpdate;
        public int counter;

        private float maxYPoint;

        public JumpData(GameObject thisPlayer, Vector2 initDirection)
        {
            if (Mathf.Abs(initDirection.x) > 0.1f)
            {
                InitializeLateralJumpPoints();
            }
            else
            {
                InitializeStandingJumpPoints();
            }
            player = thisPlayer;
            direction = initDirection;
            counter = 0;
        }

        private void InitializeLateralJumpPoints()
        {
            velocityPerFixedUpdate = new Vector2[26];
            velocityPerFixedUpdate[0] = new Vector2(8f/2f, 8f/3f);
            velocityPerFixedUpdate[1] = new Vector2(8f/2f, 8f/3f);
            velocityPerFixedUpdate[2] = new Vector2(8f/2f, 8f/2f);
            velocityPerFixedUpdate[3] = new Vector2(8f/2f, 8f/2f);
            velocityPerFixedUpdate[4] = new Vector2(8f/2f, 8f/2f);
            velocityPerFixedUpdate[5] = new Vector2(8f/2f, 8f/2f);
            velocityPerFixedUpdate[6] = new Vector2(8f/2f, 8f/1f);
            velocityPerFixedUpdate[7] = new Vector2(8f/2f, 8f/1f);
            velocityPerFixedUpdate[8] = new Vector2(8f/2f, 8f/1f);
            velocityPerFixedUpdate[9] = new Vector2(8f/2f, 8f/1f);
            velocityPerFixedUpdate[10] = new Vector2(8f/2f, 8f/1f);
            velocityPerFixedUpdate[11] = new Vector2(8f/2f, 0f);
            velocityPerFixedUpdate[12] = new Vector2(8f/2f, 0f);
            velocityPerFixedUpdate[13] = new Vector2(8f/2f, 0f);
            velocityPerFixedUpdate[14] = new Vector2(8f/2f, 0f);
            velocityPerFixedUpdate[15] = new Vector2(8f/2f, 8f/1f);
            velocityPerFixedUpdate[16] = new Vector2(8f/2f, 8f/1f);
            velocityPerFixedUpdate[17] = new Vector2(8f/2f, 8f/1f);
            velocityPerFixedUpdate[18] = new Vector2(8f/2f, 8f/1f);
            velocityPerFixedUpdate[19] = new Vector2(8f/2f, 8f/1f);
            velocityPerFixedUpdate[20] = new Vector2(8f/2f, 8f/2f);
            velocityPerFixedUpdate[21] = new Vector2(8f/2f, 8f/2f);
            velocityPerFixedUpdate[22] = new Vector2(8f/2f, 8f/2f);
            velocityPerFixedUpdate[23] = new Vector2(8f/2f, 8f/2f);
            velocityPerFixedUpdate[24] = new Vector2(8f/2f, 8f/4f);
            velocityPerFixedUpdate[25] = new Vector2(0f, 8f/2f);
        }
        
        private void InitializeStandingJumpPoints()
        {
            velocityPerFixedUpdate = new Vector2[25];
            velocityPerFixedUpdate[0] = new Vector2(0f, 8f/3f);
            velocityPerFixedUpdate[1] = new Vector2(0f, 8f/3f);
            velocityPerFixedUpdate[2] = new Vector2(0f, 8f/2f);
            velocityPerFixedUpdate[3] = new Vector2(0f, 8f/2f);
            velocityPerFixedUpdate[4] = new Vector2(0f, 8f/2f);
            velocityPerFixedUpdate[5] = new Vector2(0f, 8f/2f);
            velocityPerFixedUpdate[6] = new Vector2(0f, 8f/1f);
            velocityPerFixedUpdate[7] = new Vector2(0f, 8f/1f);
            velocityPerFixedUpdate[8] = new Vector2(0f, 8f/1f);
            velocityPerFixedUpdate[9] = new Vector2(0f, 8f/1f);
            velocityPerFixedUpdate[10] = new Vector2(0f, 0f);
            velocityPerFixedUpdate[11] = new Vector2(0f, 0f);
            velocityPerFixedUpdate[12] = new Vector2(0f, 0f);
            velocityPerFixedUpdate[13] = new Vector2(0f, 0f);
            velocityPerFixedUpdate[14] = new Vector2(0f, 0f);
            velocityPerFixedUpdate[15] = new Vector2(0f, 8f/1f);
            velocityPerFixedUpdate[16] = new Vector2(0f, 8f/1f);
            velocityPerFixedUpdate[17] = new Vector2(0f, 8f/1f);
            velocityPerFixedUpdate[18] = new Vector2(0f, 8f/1f);
            velocityPerFixedUpdate[19] = new Vector2(0f, 8f/1f);
            velocityPerFixedUpdate[20] = new Vector2(0f, 8f/2f);
            velocityPerFixedUpdate[21] = new Vector2(0f, 8f/2f);
            velocityPerFixedUpdate[22] = new Vector2(0f, 8f/2f);
            velocityPerFixedUpdate[23] = new Vector2(0f, 8f/2f);
            velocityPerFixedUpdate[24] = new Vector2(0f, 8f/4f);
        }
    }
}