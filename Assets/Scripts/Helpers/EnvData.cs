using UnityEngine;

namespace GreenBean.Helpers
{
    public class EnvData
    {
        public GameObject player;
        public float wallCastDistance;
        public float groundCastDistance;
        public bool rightBlocked;
        public bool leftBlocked;
        public bool grounded;
        public bool canJump;
        public Vector2 lastGroundedPosition;
        public LayerMask whatIsGround;
        public BoxCollider2D groundCollider;
        public BoxCollider2D wallCollider;


        public EnvData(GameObject thisPlayer, BoxCollider2D playerGroundCollider, BoxCollider2D playerWallCollider, float wallCastDist, float groundCastDist, LayerMask groundMask)
        {
            player = thisPlayer;
            rightBlocked = false;
            leftBlocked = false;
            grounded = false;
            wallCastDistance = wallCastDist;
            groundCastDistance = groundCastDist;
            whatIsGround = groundMask;
            groundCollider = playerGroundCollider;
            wallCollider = playerWallCollider;
            
            // setting the initial grounded position very low so that the player
            // doesn't just instantly die when they spawn in, if they spawn in above the
            // ground for some reason
            lastGroundedPosition = new Vector2(0,-300);
        }
        
        public void Update()
        {
            rightBlocked = false;
            leftBlocked = false;
            IsBlocked(Vector2.right, wallCastDistance);
            IsBlocked(Vector2.left, wallCastDistance);
            IsGrounded(groundCastDistance);
            if (grounded)
            {
                lastGroundedPosition = player.transform.position;
            }
        }
        
        public void IsBlocked(Vector2 direction, float distance)
        {
            if (Physics2D.BoxCast(wallCollider.bounds.center, wallCollider.size, 0f, direction, distance, whatIsGround))
            {
                if (direction.x > 0)
                {
                    rightBlocked = true;
                }
                else if (direction.x < 0)
                {
                    leftBlocked = true;
                }
            }
        }
        
        public void IsGrounded(float distance)
        {
            if (Physics2D.BoxCast(groundCollider.bounds.center, groundCollider.size, 0f, Vector2.down, distance, whatIsGround))
            {
                grounded = true;
            }
            else
            {
                grounded = false;
            }
        }
    }
}