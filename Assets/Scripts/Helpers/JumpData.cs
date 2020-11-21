using UnityEngine;

namespace Com.Technitaur.GreenBean.Helpers
{
    public class JumpData
    {
        public float[] yPpfTable = new float[25];
        public float xdir;
        public int counter;
        public bool hasPeaked;

        public float XPPF { get { if ( xdir != 0 ) { return 2; } else { return 0; } } }
        public float YPPF
        {
            get
            {
                float curPpf = yPpfTable[counter];
                if (counter < yPpfTable.Length - 1)
                    counter++;
                if (counter > 10 && !hasPeaked)
                    hasPeaked = true;
                return curPpf;
            }
        }
        
        public JumpData(float dir)
        {
            xdir = dir;
            InitializeArray();
            counter = 0;
        }
        
        public void InitializeArray()
        {
            yPpfTable = new float[] {
                3, 3,
                2, 2, 2, 2,
                1, 1, 1, 1, 1,
                0, 0, 0, 0,
                -1, -1, -1, -1, -1,
                -2, -2, -2, -2,
                -4
            };
        }
        
        /*
        public Vector2 GetNextChange()
        {
            if (counter >= yPpfTable.Length)
                counter = yPpfTable.Length - 1;

            float yUnits = yPpfTable[counter] / 8f;

            if (yUnits == 0 && !hasPeaked)
                hasPeaked = true;

            float xUnits = (xdir * 2) / 8f;
            counter++;
            return new Vector2(xUnits, yUnits);
        }
        */
    }
}