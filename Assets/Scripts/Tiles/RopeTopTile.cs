using UnityEngine;
using UnityEngine.Tilemaps;
using Com.Technitaur.GreenBean.Handlers;

namespace Com.Technitaur.GreenBean
{
    [CreateAssetMenu(fileName = "RopeTopTile", menuName = "MRTiles/RopeTopTile")]
    public class RopeTopTile : TileBase
    {
        public Sprite sprite;
        public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
        {
            base.GetTileData(position, tilemap, ref tileData);
            tileData.sprite = sprite;
        }
    }
}