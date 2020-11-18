using UnityEngine;

public class EnvironmentCheck : MonoBehaviour
{
    [Header("LayerMasks")]
    public LayerMask whatIsRopes;
    public LayerMask whatIsDoors;
    public LayerMask whatIsLadders;
    public LayerMask whatIsCollectibles;
    public LayerMask whatIsGround;
    public LayerMask whatIsBelts;
    
    [Header("Player Data")]
    public Transform wallCastOrigin;
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
            return Physics2D.Raycast(transform.position, Vector2.down, 0.05f, whatIsGround);
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
        Vector2 difference = dest - (Vector2) source;
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
}
