using UnityEngine;

namespace GreenBean.Helpers
{
    public class JumpData
    {
        public float[] pixelChange = new float[25];
        public float xdir;
        public int counter;
        
        public JumpData(float dir)
        {
            xdir = dir;
            InitializeArray();
            counter = 0;
        }
        
        public void InitializeArray()
        {
            pixelChange = new float[] {
                3, 3,
                2, 2, 2, 2,
                1, 1, 1, 1, 1,
                0, 0, 0, 0,
                -1, -1, -1, -1, -1,
                -2, -2, -2, -2,
                -4
            };
        }
        
        public Vector2 GetNextChange()
        {
            if (counter >= pixelChange.Length)
                counter = pixelChange.Length - 1;

            float yUnits = pixelChange[counter] / 8f;
            float xUnits = (xdir * 2) / 8f;
            counter++;
            return new Vector2(xUnits, yUnits);
        }
    }
}