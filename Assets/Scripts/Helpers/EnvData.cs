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
        public LayerMask whatIsGround;
        public BoxCollider2D groundCollider;


        public EnvData(GameObject thisPlayer, BoxCollider2D playerGroundCollider, float wallCastDist, float groundCastDist, LayerMask groundMask)
        {
            player = thisPlayer;
            rightBlocked = false;
            leftBlocked = false;
            grounded = false;
            wallCastDistance = wallCastDist;
            groundCastDistance = groundCastDist;
            whatIsGround = groundMask;
            groundCollider = playerGroundCollider;
        }
        
        public void Update()
        {
            rightBlocked = false;
            leftBlocked = false;
            IsBlocked(Vector2.right, wallCastDistance);
            IsBlocked(Vector2.left, wallCastDistance);
            IsGrounded(groundCastDistance);
        }
        
        public void IsBlocked(Vector2 direction, float distance)
        {
            if (Physics2D.BoxCast(groundCollider.bounds.center, groundCollider.size, 0f, direction, distance, whatIsGround))
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