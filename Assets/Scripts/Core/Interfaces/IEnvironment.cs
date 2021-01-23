using UnityEngine;

namespace Com.Technitaur.GreenBean.Core
{
    public interface IEnvironment
    {
        bool IsOnFire { get; }
        bool CanMoveRight { get; }
        bool CanMoveLeft { get; }
        bool IsGrounded { get; }
        bool CanSlide { get; }
        bool CanClimbUpRope { get; }
        bool CanClimbDownRope { get; }
        bool CanClimbDownLadder { get; }
        bool CanClimbUpLadder { get; }
        bool LadderSnapRight { get; }
        bool IsOnLeftBelt { get; }
        bool IsOnRightBelt { get; }
        Vector2Int CenterXSnap(Vector2Int pos);
        Vector2Int CenterXYSnap(Vector2Int pos);
        Vector2Int HorizontalSnap(Vector2Int pos, bool right);
    }
}
