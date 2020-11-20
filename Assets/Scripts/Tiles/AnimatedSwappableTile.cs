using UnityEngine;
using UnityEngine.Tilemaps;
using Com.Technitaur.GreenBean.Handlers;
using System.Collections.Generic;

namespace Com.Technitaur.GreenBean
{
    [CreateAssetMenu(fileName = "AnimatedSwappableTile", menuName = "MRTiles/Animated Swappable Tile")]
    public class AnimatedSwappableTile : TileBase
    {
        public Dictionary<int, Sprite[]> animatedSprites;
        public float animationSpeed;
        public float animationStartTime;

        public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
        {
            int level = 0;
            RoomController controller = tilemap.GetComponent<RoomController>();
            if (controller != null && controller.level > 0)
                level = controller.level - 1;

            if (animatedSprites != null && animatedSprites.Count > 0)
            {
                tileData.sprite = animatedSprites[level][animatedSprites[level].Length - 1];
            }
            tileData.colliderType = Tile.ColliderType.Grid;
        }

        public override bool GetTileAnimationData(Vector3Int position, ITilemap tilemap, ref TileAnimationData tileAnimationData)
        {
            int level = 0;
            RoomController controller = tilemap.GetComponent<RoomController>();
            if (controller != null && controller.level > 0)
                level = controller.level - 1;

            if (animatedSprites[level] != null && animatedSprites[level].Length > 0)
            {
                tileAnimationData.animatedSprites = animatedSprites[level];
                tileAnimationData.animationSpeed = animationSpeed;
                tileAnimationData.animationStartTime = animationStartTime;
                return true;
            }
            return false;
        }
    }
}