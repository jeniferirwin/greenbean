using UnityEngine;
using UnityEngine.Tilemaps;
using System;
using System.Collections.Generic;

namespace Com.Technitaur.GreenBean.Tilemaps
{
    [CreateAssetMenu(fileName = "HazardTile", menuName = "Hazard Tile", order = 1)]
    public class HazardTile : CustomTile, ICustomTile
    {
        public Sprite[] frames;
        public int speed;
        public int frameStart = 0;
        public bool reverse;
        
        public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
        {
            tileData.sprite = frames[frameStart];
        }

        public override bool GetTileAnimationData(Vector3Int position, ITilemap tilemap, ref TileAnimationData tileAnimationData)
        {
            tileAnimationData.animatedSprites = HazardTileArray(frameStart);
            tileAnimationData.animationSpeed = speed;
            tileAnimationData.animationStartTime = 0;
            return true;
        }

        public Sprite[] HazardTileArray(int start)
        {
            Sprite[] tiles = new Sprite[frames.Length];
            List<Sprite> rearranged = new List<Sprite>();
            for (int i = start; i < frames.Length; i++)
            {
                rearranged.Add(frames[i]);
            }
            for (int i = 0; i < start; i++)
            {
                rearranged.Add(frames[i]);
            }
            if (reverse) rearranged.Reverse();
            rearranged.CopyTo(tiles,0);
            return tiles;
        }
    }
}
