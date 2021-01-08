using UnityEngine;
using UnityEngine.Tilemaps;

namespace Com.Technitaur
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
