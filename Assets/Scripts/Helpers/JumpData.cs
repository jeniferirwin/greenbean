using UnityEngine;

namespace GreenBean.Helpers
{
    public class JumpData
    {
        public GameObject player;
        public Vector2 direction;
        public Vector2[] movementPerFixedUpdate;
        public int counter;
        
        public bool IsLateral
        {
            get
            {
                if (Mathf.Abs(direction.x) > 0.1f)
                    return true;
                else
                    return false;
            }
        }

        private float maxYPoint;

        public JumpData(GameObject thisPlayer, float initXDirection)
        {
            if (initXDirection < -0.1)
                direction = new Vector2(0,-1f);
            else if (initXDirection > 0.1)
                direction = new Vector2(0,1f);
            if (IsLateral)
            {
                InitializeLateralJumpPoints();
            }
            else
            {
                InitializeStandingJumpPoints();
            }
            player = thisPlayer;
            counter = 0;
        }

        private void InitializeLateralJumpPoints()
        {
            movementPerFixedUpdate = new Vector2[25];
            for (int i = 0; i < movementPerFixedUpdate.Length; i++)
            {
                float xMov = 2f;
                float yMov;
                if (i < 2)
                    yMov = 3f;
                else if (i >= 2 && i < 6)
                    yMov = 2f;
                else if (i >= 6 && i < 11)
                    yMov = 1f;
                else if (i >= 11 && i < 15)
                    yMov = 0f;
                else if (i >= 15 && i < 20)
                    yMov = -1f;
                else if (i >= 20 && i < 24)
                    yMov = -2f;
                else if (i == 24)
                    yMov = -3f;
                else
                    yMov = -2f;
                
                movementPerFixedUpdate[i] = new Vector2(xMov/8f, yMov/8f);
            }
            /*
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
            */
        }
        
        private void InitializeStandingJumpPoints()
        {
            movementPerFixedUpdate = new Vector2[24];
            for (int i = 0; i < movementPerFixedUpdate.Length; i++)
            {
                float xMov = 0f;
                float yMov;
                if (i < 2)
                    yMov = 3f;
                else if (i >= 2 && i < 6)
                    yMov = 2f;
                else if (i >= 6 && i < 10)
                    yMov = 1f;
                else if (i >= 10 && i < 15)
                    yMov = 0f;
                else if (i >= 15 && i < 20)
                    yMov = -1f;
                else if (i >= 20 && i < 24)
                    yMov = -2f;
                else if (i == 24)
                    yMov = -3f;
                else
                    yMov = -2f;
                
                movementPerFixedUpdate[i] = new Vector2(xMov/8f, yMov/8f);
            }
            /*
            velocityPerFixedUpdate = new Vector2[25];
            velocityPerFixedUpdate[0] = new Vector2(0f, 8.00f/3.00f);
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
            for (int i = 0; i < velocityPerFixedUpdate.Length; i++)
            {
                Vector2 vel = velocityPerFixedUpdate[i];
                Debug.Log("Added translation: " + vel.x + " / " + vel.y);
            }
            */
        }
    }
}