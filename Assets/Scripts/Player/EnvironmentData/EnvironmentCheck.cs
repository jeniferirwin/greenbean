using UnityEngine;
using GreenBean.C64;

public class EnvironmentCheck : MonoBehaviour
{
    [Header("LayerMasks")]
    public LayerMask whatIsRopes;
    public LayerMask whatIsDoors;
    public LayerMask whatIsLadders;
    public LayerMask whatIsLadderTops;
    public LayerMask whatIsLadderBottoms;
    public LayerMask whatIsRopeTops;
    public LayerMask whatIsPoles;
    public LayerMask whatIsCollectibles;
    public LayerMask whatIsGround;
    public LayerMask whatIsLeftBelts;
    public LayerMask whatIsRightBelts;

    [Header("Player Data")]
    public Transform wallCastOrigin;
    public Transform climbUpChecker;
    public Transform climbDownChecker;
    public Vector2 lastPosition;

    private Vector2 lastPos;

    public bool RightBlocked
    {
        get
        {
            return Physics2D.Raycast(wallCastOrigin.transform.position, Vector2.right, 1f, whatIsGround);
        }
    }

    public bool LeftBlocked
    {
        get
        {
            return Physics2D.Raycast(wallCastOrigin.transform.position, Vector2.left, 1f, whatIsGround);
        }
    }

    public bool Grounded
    {
        get
        {
            return Physics2D.Raycast(transform.position, Vector2.down, PixConvert.PixelsToUnits(1), whatIsGround);
        }
    }

    public bool MovingUp
    {
        get
        {
            return transform.position.y > LastPosition.y;
        }
    }

    public bool MovingDown
    {
        get
        {
            return transform.position.y < LastPosition.y;
        }
    }

    public bool MovingRight
    {
        get
        {
            return transform.position.x > LastPosition.x;
        }
    }

    public bool MovingLeft
    {
        get
        {
            return transform.position.x < LastPosition.x;
        }
    }

    public Vector2 LastPosition
    {
        get
        {
            return lastPos;
        }
        set
        {
            lastPos = value;
        }
    }

    public Vector2 CollisionIntersect(Vector2 source, Vector2 dest)
    {
        RaycastHit2D hit;
        Vector2 difference = dest - (Vector2)source;
        hit = Physics2D.Raycast(source, difference.normalized, difference.magnitude, whatIsGround);
        if (hit.rigidbody != null)
        {
            return hit.point;
        }
        else
        {
            return new Vector2(-32500, 32500);
        }
    }

    public bool LadderBelow
    {
        get
        {
            return Physics2D.CircleCast(climbDownChecker.position, PixConvert.PixelsToUnits(1), Vector2.zero, 0f, whatIsLadders);
        }
    }
    
    public bool LadderAbove
    {
        get
        {
            return Physics2D.CircleCast(climbUpChecker.position, PixConvert.PixelsToUnits(1), Vector2.zero, 0f, whatIsLadders);
        }
    }
    
    public bool AtPole
    {
        get
        {
            return Physics2D.CircleCast(transform.position, PixConvert.PixelsToUnits(1), Vector2.zero, 0f, whatIsPoles);
        }
    }

    public bool AtRope
    {
        get
        {
            return Physics2D.CircleCast(transform.position, PixConvert.PixelsToUnits(1), Vector2.zero, 0f, whatIsRopes);
        }
    }

    public bool OnRightBelt
    {
        get
        {
            return Physics2D.Raycast(transform.position, Vector2.down, PixConvert.PixelsToUnits(1), whatIsRightBelts);
        }
    }

    public bool OnLeftBelt
    {
        get
        {
            return Physics2D.Raycast(transform.position, Vector2.down, PixConvert.PixelsToUnits(1), whatIsLeftBelts);
        }
    }
}
