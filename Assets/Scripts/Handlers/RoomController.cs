using UnityEngine;
using UnityEngine.Tilemaps;

namespace Com.Technitaur.GreenBean.Handlers
{
    public class RoomController : MonoBehaviour
    {
        public int level;
        
        public void Start()
        {
            gameObject.GetComponent<Tilemap>().RefreshAllTiles();
        }
    }
}