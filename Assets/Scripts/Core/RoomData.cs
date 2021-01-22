using UnityEngine;

namespace Com.Technitaur.GreenBean.Core
{
    public class RoomData : MonoBehaviour
    {
        public Room RoomName { get { return roomName; } }
        
        [SerializeField] private Room roomName = Room.A09;
    }
}
