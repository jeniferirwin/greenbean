using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public LayerMask whatIsGround;
    public float moveSpeed;
    public float jumpHeight;
    public float groundCastLength;
    public float wallCastLength;
    public SpriteRenderer playerSprite;

    private Vector2 lastPos;
    private Vector2 jumpingDirection;
    private bool facingRight;
    private Color defaultColor;
    private Vector2 jumpStart;
    private bool isJumping;
    private bool jumpingUp;
    private bool wantsMoveRight;
    private bool wantsMoveLeft;
    private bool wantsJump;

    private void Start()
    {
        jumpingDirection = new Vector2(0,0);
        facingRight = false;
        defaultColor = playerSprite.color;
    }
    
    private bool IsGrounded()
    {
        if (Physics2D.Raycast(transform.position,Vector2.down,groundCastLength,whatIsGround))
            return true;
        else
            return false;
    }
    
    private void GetWantsJump()
    {
        if (Input.GetKey(KeyCode.Space) && !isJumping)
        {
            wantsJump = true;
        }
    }
    
    private void GetWantsMove()
    {
        if (Input.GetKey(KeyCode.D))
        {
            facingRight = true;
            wantsMoveRight = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            facingRight = false;
            wantsMoveLeft = true;
        }
    }
    
    private void Update()
    {
        if (IsGrounded())
        {
            isJumping = false;
            jumpingUp = false;
            jumpingDirection = Vector2.zero;
        }

        GetWantsMove();
        GetWantsJump();

        if (wantsJump && !isJumping)
        {
            jumpStart = transform.position;
            isJumping = true;
        }

        lastPos = transform.position;

        wantsMoveLeft = false;
        wantsMoveRight = false;
        wantsJump = false;
    }
    
    private bool WallCast(Vector2 direction)
    {
        if (Physics2D.Raycast(transform.position,direction,wallCastLength,whatIsGround))
            return true;
        else
            return false;
    }
}
