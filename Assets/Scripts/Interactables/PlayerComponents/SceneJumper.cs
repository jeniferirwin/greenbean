using UnityEngine;
using Com.Technitaur.GreenBean.Core;

namespace Com.Technitaur.GreenBean.Interactables
{
    public class SceneJumper : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<ITransitionZone>(out ITransitionZone zone))
            {
                var player = gameObject.transform.parent.gameObject;
                RoomLoader.Load(zone.Room, zone.Direction, player);
            }
        }
    }
}
