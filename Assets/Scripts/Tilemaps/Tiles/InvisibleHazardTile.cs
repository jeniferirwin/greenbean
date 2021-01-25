using UnityEngine;
using UnityEngine.Tilemaps;
using System;
using System.Collections.Generic;

namespace Com.Technitaur.GreenBean.Tilemaps
{
    [CreateAssetMenu(fileName = "InvisibleHazardTile", menuName = "Invisible Hazard Tile", order = 1)]
    public class InvisibleHazardTile : CustomTile, ICustomTile
    {
        public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
        {
            tileData.sprite = null;
        }
    }
}
