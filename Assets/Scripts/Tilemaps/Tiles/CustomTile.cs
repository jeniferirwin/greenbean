using UnityEngine;
using UnityEngine.Tilemaps;

namespace Com.Technitaur.GreenBean.Tilemaps
{
    [CreateAssetMenu(fileName = "CustomTile", menuName = "Custom Tile", order = 1)]
    public class CustomTile : TileBase
    {
        public Vector3Int pos;
        public Sprite[] sprites;
        public bool isLadder;
        public bool isHazard;
        public bool isRope;
        public bool isPole;
        public bool isSolid;
        public bool isSemiSolid;
        public bool isDoor;
        public bool isClosed;
        public bool isCollectible;
        public bool isLeftBelt;
        public bool isRightBelt;
        public int level;

        public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
        {
            tileData.sprite = sprites[0];
        }

        public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go)
        {
            pos = position;
            return true;
        }
    }
}
