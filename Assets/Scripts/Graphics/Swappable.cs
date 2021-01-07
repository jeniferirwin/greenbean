using UnityEngine;
using Com.Technitaur.GreenBean.Core;

namespace Com.Technitaur.GreenBean.Graphics
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Swappable : MonoBehaviour
    {
        private void Start()
        {
            var room = GameObject.FindGameObjectWithTag("RoomData");
            var roomData = room.GetComponent<RoomData>();
            var renderer = GetComponent<SpriteRenderer>();
            PaletteSwap.ColorSwap(renderer, roomData.level);
        }
    }
}
