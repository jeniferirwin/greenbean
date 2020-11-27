using UnityEngine;
using UnityEngine.Tilemaps;

namespace Com.Technitaur.GreenBean.Tilemaps
{
    [CreateAssetMenu(fileName = "LeveledStaticTile", menuName = "Leveled Static Tile", order = 1)]
    public class LeveledStaticTile : CustomTile, ICustomTile
    {
        public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
        {
            int section = level * 8;
            Rect rect = new Rect(section, 0, 8, 8);
            Vector2 pivot = new Vector2(0.5f, 0.5f);
            int ppu = 1;
            tileData.sprite = Sprite.Create(texture, rect, pivot, ppu, 0, SpriteMeshType.Tight);
        }
    }
}
