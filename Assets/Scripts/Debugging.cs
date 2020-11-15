using UnityEngine;
using GreenBean.Player;

namespace GreenBean.Debugging
{
    public class Debugging : MonoBehaviour
    {
        [Header("Setup")]
        public Movement movement;
        public SpriteRenderer spriteRenderer;
        public LayerMask whatIsGround;
        [Header("Colors")]
        public Color defaultColor;
        public Color groundedColor;
        public Color leftBlockedColor;
        public Color rightBlockedColor;
        [Header("Options")]
        public bool debugGroundCast;
        public bool debugLeftWallCast;
        public bool debugRightWallCast;
        
        public class BoxColliderCast
        {
            public Vector2 origin;
            public Vector2 size;
            public float angle;
            public Vector2 direction;
            public float distance;
            public LayerMask layerMask;
            public bool didHit;

            public BoxColliderCast(BoxCollider2D collider, Vector2 dir, float dist, LayerMask mask)
            {
                origin = collider.bounds.center;
                size = collider.size;
                angle = 0f;
                direction = dir;
                distance = dist;
                layerMask = mask;

                if (Physics2D.BoxCast(origin,size,angle,direction,distance,layerMask))
                {
                    didHit = true;
                }
            }
        }
        
        private void OnDrawGizmos()
        {
            spriteRenderer.color = defaultColor;

            if (debugGroundCast)
            {
                DrawBoxCast(movement.groundCollider, Vector2.down, movement.groundCastDistance, Color.green);
                BoxColliderCast cast = new BoxColliderCast(movement.groundCollider,Vector2.down,movement.groundCastDistance,whatIsGround);
                if (cast.didHit)
                {
                    spriteRenderer.color = Color.green;
                }
            }
            if (debugLeftWallCast)
            {
                DrawBoxCast(movement.wallCollider, Vector2.left, movement.wallCastDistance, Color.red);
                BoxColliderCast cast = new BoxColliderCast(movement.wallCollider,Vector2.left,movement.wallCastDistance,whatIsGround);
                if (cast.didHit)
                {
                    spriteRenderer.color = Color.red;
                }
            }
            if (debugRightWallCast)
            {
                DrawBoxCast(movement.wallCollider, Vector2.right, movement.wallCastDistance, Color.blue);
                BoxColliderCast cast = new BoxColliderCast(movement.wallCollider,Vector2.right,movement.wallCastDistance,whatIsGround);
                if (cast.didHit)
                {
                    spriteRenderer.color = Color.blue;
                }
            }
        }
        
        private void DrawBoxCast(BoxCollider2D collider, Vector2 direction, float distance, Color color)
        {
            Vector2 upperLeft = collider.bounds.center;
            upperLeft.x -= collider.bounds.extents.x;
            upperLeft.y += collider.bounds.extents.y;
            Vector2 upperRight = collider.bounds.center;
            upperRight.x += collider.bounds.extents.x;
            upperRight.y += collider.bounds.extents.y;
            Vector2 lowerLeft = collider.bounds.center;
            lowerLeft.x -= collider.bounds.extents.x;
            lowerLeft.y -= collider.bounds.extents.y;
            Vector2 lowerRight = collider.bounds.center;
            lowerRight.x += collider.bounds.extents.x;
            lowerRight.y -= collider.bounds.extents.y;
            
            upperLeft += direction * distance;
            upperRight += direction * distance;
            lowerLeft += direction * distance;
            lowerRight += direction * distance;

            Debug.DrawLine(upperLeft, upperRight, color);
            Debug.DrawLine(upperLeft, lowerLeft, color);
            Debug.DrawLine(upperRight, lowerRight, color);
            Debug.DrawLine(lowerLeft, lowerRight, color);
        }
    }
}