using UnityEngine;

namespace Com.Technitaur.GreenBean.Core
{
    public class RoomData : MonoBehaviour, IRoom
    {
        public int level;

        public int Level
        {
            get
            {
                if (level > 0 && level <= 10)
                    return level;
                else
                    return 1;
            }
        }
    }
}
