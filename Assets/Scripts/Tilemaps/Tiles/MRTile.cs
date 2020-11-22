using UnityEngine;
using UnityEngine.Tilemaps;
using System;

namespace Com.Technitaur.GreenBean
{
    [CreateAssetMenu(fileName = "MRTile", menuName = "MRTiles/MRTile")]
    public class MRTile : TileBase
    {
        public int level = 0;
        public InteractableType objType;
        public Sprite[] sprites;
        public Quadrant quadrant;
        public TileType tileType;
        public Sprite[] animatedSprites;
        public Solidity solidity;

        public enum Quadrant
        {
            None,
            UpperLeft,
            UpperRight,
            LowerLeft,
            LowerRight
        }

        public enum InteractableType
        {
            ClosedDoor,
            OpenDoor,
            Key,
            Coin,
            Wand,
            Torch
        }
        
        public enum TileType {
            Interactable,
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
                tileData.sprite = sprites[level - 1];
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