using UnityEngine;
using Com.Technitaur.GreenBean.Core;
using UnityEngine.Tilemaps;

namespace Com.Technitaur.GreenBean.Graphics
{
    [RequireComponent(typeof(TilemapRenderer))]
    public class SwappableTilemap : MonoBehaviour
    {
        private void Start()
        {
            var room = GameObject.FindGameObjectWithTag("RoomData");
            var roomData = room.GetComponent<RoomData>();
            var renderer = GetComponent<TilemapRenderer>();
        }
    }
}
