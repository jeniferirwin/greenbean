using UnityEngine;
using UnityEngine.Tilemaps;
using Com.Technitaur.GreenBean.Handlers;

namespace Com.Technitaur.GreenBean
{
    [CreateAssetMenu(fileName = "PartialBrickTile", menuName = "MRTiles/PartialBrickTile")]
    public class PartialBrickTile : TileBase
    {
        public Sprite[] sprites;

        public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
        {
            base.GetTileData(position, tilemap, ref tileData);
            RoomController controller = tilemap.GetComponent<RoomController>();
            if (controller != null)
                tileData.sprite = sprites[controller.level - 1];
            else
                tileData.sprite = sprites[0];
        }
    }
}