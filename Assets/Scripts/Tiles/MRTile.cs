using UnityEngine;
using UnityEngine.Tilemaps;
using Com.Technitaur.GreenBean.Handlers;
using System;

namespace Com.Technitaur.GreenBean
{
    [CreateAssetMenu(fileName = "MRTile", menuName = "MRTiles/MRTile")]
    public class MRTile : TileBase
    {
        public Sprite[] sprites;
        public enum TileType {
            Brick,
            PartialBrick,
            LadderTopLeft,
            LadderLeft,
            LadderTopRight,
            LadderRight, 
            LadderBottomLeft, 
            LadderBottomRight, 
            RopeTop, 
            RopeMiddle, 
            RopeBottom, 
            Pole, 
            PoleBottom, 
            UISide, 
            UIBottom,
            Decoration,
            LeftBeltLeft,
            LeftBeltMiddle,
            LeftBeltRight,
            RightBeltLeft,
            RightBeltMiddle,
            RightBeltRight,
            SemiSolidBrick
        };
        public enum Solidity
        {
            None,
            Semisolid,
            Solid
        }
        public TileType tileType;
        public Sprite[] animatedSprites;
        public Solidity solidity;

        public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
        {
            base.GetTileData(position, tilemap, ref tileData);
            if (sprites.Length == 0 && animatedSprites.Length == 0)
            {
                return;
            }
            if (sprites.Length == 0 && animatedSprites.Length > 0)
            {
                tileData.sprite = animatedSprites[0];
                return;
            }
            if (sprites.Length > 1)
            {
                RoomController controller = tilemap.GetComponent<RoomController>();
                if (controller != null)
                    tileData.sprite = sprites[controller.level - 1];
                else
                    tileData.sprite = sprites[0];
            }
            else
            {
                tileData.sprite = sprites[0];
            }
        }

        public override bool GetTileAnimationData(Vector3Int position, ITilemap tilemap, ref TileAnimationData tileAnimationData)
        {
            if (animatedSprites != null && animatedSprites.Length > 1)
            {
                Sprite[] copied = new Sprite[animatedSprites.Length];
                animatedSprites.CopyTo(copied, 0);
                if (tileType == TileType.LeftBeltLeft || tileType == TileType.LeftBeltMiddle || tileType == TileType.LeftBeltRight)
                    Array.Reverse(copied);
                tileAnimationData.animatedSprites = copied;
                tileAnimationData.animationSpeed = 25;
                tileAnimationData.animationStartTime = 0;
                return true;
            }
            return false;
        }
    }
}