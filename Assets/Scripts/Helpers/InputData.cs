using UnityEngine;

namespace GreenBean.Helpers
{
    public class InputData
    {
        public float bufferLength;
        public float curBufferTimer;
        public bool cycleFinished;

        public Vector2 currentAxis;
        public bool hasJump;

        public InputData(float bufferLen)
        {
            bufferLength = bufferLen;
            currentAxis = Vector2.zero;
        }

        public void GetAxis(Vector2 input)
        {
            if (input.x < 0)
            {
                currentAxis = Vector2.right;
            }
            else if (input.x > 0)
            {
                currentAxis = Vector2.left;
            }
            else if (input.y > 0)
            {
                currentAxis = Vector2.up;
            }
            else if (input.y < 0)
            {
                currentAxis = Vector2.down;
            }
            else
            {
                currentAxis = Vector2.zero;
            }
        }

        public void Update()
        {
            if (curBufferTimer > 0f)
            {
                curBufferTimer -= Time.deltaTime;
            }
            else
            {
                cycleFinished = true;
            }
        }
    }
}