using UnityEngine;
using UnityEngine.Tilemaps;

namespace Com.Technitaur.GreenBean.Tilemaps
{
    [CreateAssetMenu(fileName = "AnimatedCustomTile", menuName = "Animated Custom Tile", order = 1)]
    public class AnimatedCustomTile : CustomTile
    {
        public Sprite[] spritesA;
        public Sprite[] spritesB;
        public Sprite[] spritesC;
        public Sprite[] spritesD;
        public Sprite[] spritesE;
        public Sprite[] spritesF;
        public Sprite[] spritesG;
        public Sprite[] spritesH;
        public Sprite[] spritesI;
        public Sprite[] spritesJ;

        public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
        {
            tileData.sprite = spritesA[0];
        }

        public override bool GetTileAnimationData(Vector3Int position, ITilemap tilemap, ref TileAnimationData tileAnimationData)
        {
            tileAnimationData.animatedSprites = spritesA;
            tileAnimationData.animationSpeed = 25;
            tileAnimationData.animationStartTime = 0;
            return true;
        }

        public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go)
        {
            pos = position;
            return true;
        }
    }
}
