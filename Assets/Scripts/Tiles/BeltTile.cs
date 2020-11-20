using UnityEngine;
using UnityEngine.Tilemaps;
using Com.Technitaur.GreenBean.Handlers;
using System.Collections.Generic;
using System.Collections;
using System;

namespace Com.Technitaur.GreenBean
{
    [CreateAssetMenu(fileName = "BeltTile", menuName = "MRTiles/Belt Tile")]
    public class BeltTile : TileBase
    {
        public Sprite[] animatedSpritesLeft;
        public Sprite[] animatedSpritesMiddle;
        public Sprite[] animatedSpritesRight;
        public float animationSpeed;
        public float animationStartTime;
        public Direction beltDir;
        public Section beltSection;
        public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
        {
            if (beltSection == Section.Left && animatedSpritesLeft != null)
            {
                tileData.sprite = animatedSpritesLeft[0];
            }
            else if (beltSection == Section.Middle && animatedSpritesMiddle != null)
            {
                tileData.sprite = animatedSpritesMiddle[0];
            }
            if (beltSection == Section.Right && animatedSpritesRight != null)
            {
                tileData.sprite = animatedSpritesMiddle[0];
            }
            tileData.colliderType = Tile.ColliderType.Grid;
        }
        
        public override bool GetTileAnimationData(Vector3Int position, ITilemap tilemap, ref TileAnimationData tileAnimationData)
        {
            Sprite[] sprites = GetSpriteSection(beltSection);
            Sprite[] copied = new Sprite[4];
            sprites.CopyTo(copied,0);
            if (sprites != null && sprites.Length > 0)
            {
                if (beltDir == Direction.Left)
                    Array.Reverse(copied);

                tileAnimationData.animatedSprites = copied;
                tileAnimationData.animationSpeed = animationSpeed;
                tileAnimationData.animationStartTime = animationStartTime;
                return true;
            }
            return false;
        }
        
        public Sprite[] GetSpriteSection(Section section)
        {
            switch (section)
            {
                case Section.Left:
                {
                    return animatedSpritesLeft;
                }
                case Section.Middle:
                {
                    return animatedSpritesMiddle;
                }
                case Section.Right:
                {
                    return animatedSpritesRight;
                }
            }
            return null;
        }

        public enum Direction { Left, Right };
        public enum Section { Left, Middle, Right };
    }
}