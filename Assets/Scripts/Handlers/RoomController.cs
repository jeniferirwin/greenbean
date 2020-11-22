using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

namespace Com.Technitaur.GreenBean.Handlers
{
    public class RoomController : MonoBehaviour
    {
        public int level;
        public Tilemap tilemap;
        
        
        public void Start()
        {
            tilemap = GetComponent<Tilemap>();
            tilemap.RefreshAllTiles();
            // tilemap.GetTilesBlock() -- this will be useful for the multi-part tiles
            
        }
        
        public void OnCollectItem()
        {
            
        }
    }
}