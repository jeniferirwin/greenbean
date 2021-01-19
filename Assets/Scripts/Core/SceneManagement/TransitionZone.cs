using UnityEngine;

namespace Com.Technitaur.GreenBean.Core
{
    public class TransitionZone : MonoBehaviour, ITransitionZone
    {
        [SerializeField] private Room room = Room.A09;
        [SerializeField] private Direction direction = Direction.Down;

        public Room Room { get { return room; } }
        public Direction Direction { get { return direction; } }
    }
}
