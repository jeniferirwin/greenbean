﻿using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "AnimatedTile", menuName = "Animated Tile")]

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
    }

    public override bool GetTileAnimationData(Vector3Int position, ITilemap tilemap, ref TileAnimationData tileAnimationData)
    {
        if (animatedSprites != null && animatedSprites.Length > 0)
        {
            tileAnimationData.animatedSprites = animatedSprites;
            tileAnimationData.animationSpeed = animationSpeed;
            tileAnimationData.animationStartTime = animationStartTime;
            return true;
        }
        return false;
    }
}
