using UnityEngine;

namespace Com.Technitaur.GreenBean.Core
{
    public class RoomData : MonoBehaviour
    {
        public Room RoomName { get { return roomName; } }
        public Room UpLink { get { return up; } }
        public Room DownLink { get { return down; } }
        public Room LeftLink { get { return left; } }
        public Room RightLink { get { return right; } }
        
        [SerializeField] private Room roomName = Room.qasmoke;
        [SerializeField] private Room up = Room.qasmoke;
        [SerializeField] private Room down = Room.qasmoke;
        [SerializeField] private Room left = Room.qasmoke;
        [SerializeField] private Room right = Room.qasmoke;
    }
}
