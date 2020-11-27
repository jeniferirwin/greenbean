using UnityEngine;
using UnityEngine.Tilemaps;
using System;

namespace Com.Technitaur.GreenBean.Tilemaps
{
    [CreateAssetMenu(fileName = "BeltTile", menuName = "Belt Tile", order = 1)]
    public class BeltTile : CustomTile, ICustomTile
    {
        public int beltSide;
        public bool right;
        
        public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
        {
            Rect rect = sprite.rect;
            rect.Set(level * 3 * 8 + beltSide * 8, rect.y, rect.width, rect.height);
            Vector2 pivot = new Vector2(0.5f, 0.5f);
            float ppu = 1;
            Sprite newSprite = Sprite.Create(texture, rect, pivot, ppu, 0, SpriteMeshType.Tight);
            tileData.sprite = newSprite;
        }

        public override bool GetTileAnimationData(Vector3Int position, ITilemap tilemap, ref TileAnimationData tileAnimationData)
        {
            tileAnimationData.animatedSprites = BeltTileArray(texture,level,beltSide,right);
            tileAnimationData.animationSpeed = 25;
            tileAnimationData.animationStartTime = 0;
            return true;
        }

        public Sprite[] BeltTileArray(Texture2D tex, int level, int side, bool right)
        {
            Sprite[] tiles = new Sprite[4];
            int segment = level * 3 * 8 + side * 8;
            for (int i = 0; i < tiles.Length; i++)
            {
                Rect newRect = new Rect(segment, i * 8, 8f, 8f);
                Vector2 pivot = new Vector2(0.5f, 0.5f);
                int ppu = 1;
                Sprite newSprite = Sprite.Create(tex,newRect,pivot,ppu,0);
                tiles[i] = newSprite;
            }
            if (right)
            {
                Array.Reverse(tiles);
            }
            return tiles;
        }
    }
}
