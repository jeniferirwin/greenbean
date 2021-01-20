using UnityEngine;
using UnityEngine.Tilemaps;
using System;

namespace Com.Technitaur.GreenBean.Tilemaps
{
    [CreateAssetMenu(fileName = "BeltTile", menuName = "Belt Tile", order = 1)]
    public class BeltTile : CustomTile, ICustomTile
    {
        public Sprite[] frames = new Sprite[4];
        
        public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
        {
            tileData.sprite = frames[0];
        }

        public override bool GetTileAnimationData(Vector3Int position, ITilemap tilemap, ref TileAnimationData tileAnimationData)
        {
            tileAnimationData.animatedSprites = BeltTileArray(this.isRightBelt);
            tileAnimationData.animationSpeed = 50;
            tileAnimationData.animationStartTime = 0;
            return true;
        }

        public Sprite[] BeltTileArray(bool isRightBelt)
        {
            Sprite[] tiles = new Sprite[4];
            frames.CopyTo(tiles,0);
            if (!isRightBelt) Array.Reverse(tiles);
            return tiles;
        }
    }
}
