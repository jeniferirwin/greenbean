using UnityEngine;
using UnityEngine.Tilemaps;

namespace Com.Technitaur.GreenBean.Tilemaps
{
    public class InitializeLeveledTiles : MonoBehaviour
    {
        private void Start()
        {
            var map = GetComponent<Tilemap>();
            map.RefreshAllTiles();
        }
    }
}
