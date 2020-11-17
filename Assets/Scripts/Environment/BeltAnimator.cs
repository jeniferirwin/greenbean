using UnityEngine;
using UnityEngine.Tilemaps;

public class BeltAnimator : MonoBehaviour
{
    public TilemapRenderer tilemapRenderer;
    public Tilemap tilemap;
    public Tile tempTile;
    
    public void Start()
    {
        tilemap.animationFrameRate = 50;
    }
}
