using UnityEngine;
using UnityEngine.Tilemaps;

namespace Com.Technitaur.GreenBean.Tilemaps
{
    [CreateAssetMenu(fileName = "GlobalStaticTile", menuName = "Global Static Tile", order = 1)]
    public class GlobalStaticTile : CustomTile, ICustomTile
    {
        public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
        {
            tileData.sprite = sprite;
        }
    }
}
