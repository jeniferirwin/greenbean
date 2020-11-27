using UnityEngine;
using UnityEngine.Tilemaps;

namespace Com.Technitaur.GreenBean.Tilemaps
{
    [CreateAssetMenu(fileName = "CustomTile", menuName = "Custom Tile", order = 1)]
    public abstract class CustomTile : TileBase, ICustomTile
    {
        [Header("Basic")]
        public Sprite sprite;
        public Texture2D texture;
        public Vector3Int pos;
        public int level;
        [Header("Types")]
        public bool isSolid;
        public bool isSemisolid;
        public bool isLeftLadder;
        public bool isRightLadder;
        public bool isRope;
        public bool isPole;
        public bool isLeftBelt;
        public bool isRightBelt;

        public int GetLevel { get { return level; } }
        public int SetLevel { set { level = value; } }
        public bool IsLeftLadder { get { return isLeftLadder; } }
        public bool IsRightLadder { get { return isRightLadder; } }
        public bool IsPole { get { return isPole; } }
        public bool IsRope { get { return isRope; } }
        public bool IsSolid { get { return isSolid; } }
        public bool IsSemisolid { get { return isSemisolid; } }
        public bool IsLeftBelt { get { return isLeftBelt; } }
        public bool IsRightBelt { get { return isRightBelt; } }
    }
}
