using UnityEngine;
using UnityEngine.Tilemaps;

namespace Com.Technitaur.GreenBean.Tilemaps
{
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
        public bool isMountingLadder;
        public bool isRope;
        public bool isPole;
        public bool isLeftBelt;
        public bool isRightBelt;
        public bool isHazard;
        [Header("Doors")]
        public bool isCyanDoor;
        public bool isRedDoor;
        public bool isVioletDoor;

        public int GetLevel { get { return level; } }
        public int SetLevel { set { level = value; } }
        public bool IsLeftLadder { get { return isLeftLadder; } }
        public bool IsRightLadder { get { return isRightLadder; } }
        public bool IsMountingLadder { get { return isMountingLadder; } }
        public bool IsPole { get { return isPole; } }
        public bool IsRope { get { return isRope; } }
        public bool IsSolid { get { return isSolid; } }
        public bool IsSemisolid { get { return isSemisolid; } }
        public bool IsLeftBelt { get { return isLeftBelt; } }
        public bool IsRightBelt { get { return isRightBelt; } }
        public bool IsHazard { get { return isHazard; } }
        public bool IsCyanDoor { get { return isCyanDoor; } }
        public bool IsRedDoor { get { return isCyanDoor; } }
        public bool IsVioletDoor { get { return isCyanDoor; } }
    }
}
