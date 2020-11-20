using UnityEngine;
using UnityEngine.Tilemaps;
using Com.Technitaur.GreenBean.Handlers;

namespace Com.Technitaur.GreenBean
{
    [CreateAssetMenu(fileName = "MRTile", menuName = "MRTiles/MRTile")]
    public class MRTile : TileBase
    {
        public Sprite[] sprites;
        public enum TileType {
            Brick,
            PartialBrick,
            LadderTopLeft,
            LadderLeft,
            LadderTopRight,
            LadderRight, 
            LadderBottomLeft, 
            LadderBottomRight, 
            RopeTop, 
            RopeMiddle, 
            RopeBottom, 
            Pole, 
            PoleBottom, 
            UISide, 
            UIBottom,
            Decoration
        };
        public TileType tileType;

        public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
        {
            base.GetTileData(position, tilemap, ref tileData);
            if (sprites.Length > 1)
            {
                RoomController controller = tilemap.GetComponent<RoomController>();
                if (controller != null)
                    tileData.sprite = sprites[controller.level - 1];
                else
                    tileData.sprite = sprites[0];
            }
            else
            {
                tileData.sprite = sprites[0];
            }
        }
    }
}