using UnityEngine;
using UnityEngine.Tilemaps;
using Com.Technitaur.GreenBean.Handlers;
using System.Collections.Generic;

namespace Com.Technitaur.GreenBean
{
    [CreateAssetMenu(fileName = "AnimatedTile", menuName = "MRTiles/Animated Tile")]
    public class AnimatedTile : TileBase
    {
        public Sprite[] animatedSprites;
        public float animationSpeed;
        public float animationStartTime;

        public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
        {
            if (animatedSprites != null && animatedSprites.Length > 0)
            {
                tileData.sprite = animatedSprites[animatedSprites.Length - 1];
            }
            tileData.colliderType = Tile.ColliderType.Grid;
        }

        public override bool GetTileAnimationData(Vector3Int position, ITilemap tilemap, ref TileAnimationData tileAnimationData)
        {
            int level = 0;
            RoomController controller = tilemap.GetComponent<RoomController>();
            if (controller != null && controller.level > 0)
                level = controller.level - 1;

            if (animatedSprites != null && animatedSprites[animatedSprites.Length - 1])
            {
                tileAnimationData.animatedSprites = animatedSprites;
                tileAnimationData.animationSpeed = animationSpeed;
                tileAnimationData.animationStartTime = animationStartTime;
                return true;
            }
            return false;
        }
    }
}