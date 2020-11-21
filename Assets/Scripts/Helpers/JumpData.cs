using UnityEngine;

namespace Com.Technitaur.GreenBean.Helpers
{
    public class JumpData
    {
        public int[] distTable = new int[25];
        public int xdir;
        public int ydir;
        public int xdist;
        public int ydist;
        public int counter;
        public bool hasPeaked;
        
        public JumpData(int dir)
        {
            xdir = dir;
            if (xdir != 0)
                xdist = 2;
            else
                xdist = 0;
            InitializeArray();
            counter = 0;
        }
        
        public void InitializeArray()
        {
            distTable = new int[] {
                3, 3,
                2, 2, 2, 2,
                1, 1, 1, 1, 1,
                0, 0, 0, 0,
                -1, -1, -1, -1, -1,
                -2, -2, -2, -2,
                -4
            };
        }
        
        public void NextStep()
        {
            ydist = distTable[counter];
            ydir = ydist / 1;
            if (counter < distTable.Length - 1) counter++;
            if (!hasPeaked && ydist == 0)
            {
                Debug.Log("Peaked");
                hasPeaked = true;
            }
        }
    }
}