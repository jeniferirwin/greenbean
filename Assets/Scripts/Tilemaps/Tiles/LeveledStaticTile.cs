using UnityEngine;
using UnityEngine.Tilemaps;

namespace Com.Technitaur.GreenBean.Tilemaps
{
    [CreateAssetMenu(fileName = "LeveledStaticTile", menuName = "Leveled Static Tile", order = 1)]
    public class LeveledStaticTile : CustomTile, ICustomTile
    {
        public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
        {
            tileData.sprite = sprite;
        }

        public override void RefreshTile(Vector3Int position, ITilemap tilemap)
        {
            tilemap.RefreshTile(position);
        }

        public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go)
        {
            return base.StartUp(position, tilemap, go);
        }
    }
}
