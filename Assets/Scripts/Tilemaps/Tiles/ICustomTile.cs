using UnityEngine;
using UnityEngine.Tilemaps;

namespace Com.Technitaur.GreenBean.Tilemaps
{
    public interface ICustomTile
    {
        int GetLevel { get; }
        int SetLevel { set; }
        bool IsLeftLadder { get; }
        bool IsRightLadder { get; }
        bool IsRope { get; }
        bool IsSolid { get; }
        bool IsSemisolid { get; }
        bool IsPole { get; }
        bool IsLeftBelt { get; }
        bool IsRightBelt { get; }
    }
}
