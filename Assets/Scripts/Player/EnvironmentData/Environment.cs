using UnityEngine;
using UnityEngine.Tilemaps;

namespace Com.Technitaur.GreenBean
{
    public class Environment : MonoBehaviour
    {
        public GridLayout grid;
        public Tilemap map;
        public Vector2 lastFramePos;
        public float pixel = 0.125f;
        public float wallCastPixels = 8;
        public enum TileType { FullBrick, PartialBrick, LadderTopLeft, LadderTopRight, LadderLeft, LadderRight, RopeTop, Rope, Belt, Pole };
        public enum BeltType { None, Left, Right };

        public Vector2 pos { get { return (Vector2) transform.position; } }
        public Vector2 UpPixel { get { return pos + Vector2.up * pixel; } }
        public Vector2 DownPixel { get { return pos + Vector2.down * pixel; } }
        public Vector2 LeftPixel { get { return pos + Vector2.left * pixel; } }
        public Vector2 RightPixel { get { return pos + Vector2.right * pixel; } }

        public bool IsGrounded
        {
            get
            {
                Vector2 point = DownPixel;
                Vector3Int cell = grid.WorldToCell(point);
                return HasSolidTile(cell);
            }
        }
        
        public bool HasSolidTile(Vector3Int cell)
        {
            MRTile mrTile = map.GetTile<MRTile>(cell);
            BeltTile beltTile = map.GetTile<BeltTile>(cell);

            if (beltTile != null) return true;

            switch (mrTile.tileType)
            {
                case MRTile.TileType.Brick:
                case MRTile.TileType.LadderTopLeft:
                case MRTile.TileType.LadderTopRight:
                case MRTile.TileType.PartialBrick:
                case MRTile.TileType.RopeTop:
                    return true;
                default:
                    return false;
            }

        }

        public bool LeftBlocked
        {
            get
            {
                return IsBlocked(pos, Vector2.left, pixel * wallCastPixels);
            }
        }

        public bool RightBlocked
        {
            get
            {
                return IsBlocked(pos, Vector2.right, pixel * wallCastPixels);
            }
        }


        public bool IsBlocked(Vector2 pos, Vector2 dir, float distance)
        {
            float covered = 0f;
            int pixels = 0;
            bool blocked = false;
            while (Mathf.Abs(covered) < distance && !blocked)
            {
                Vector2 newPoint = pos;
                newPoint.x += dir.x * pixel * pixels;
                Vector3Int cell = grid.WorldToCell(newPoint);
                if (map.HasTile(cell))
                {
                    if (HasSolidTile(cell)) return true;
                }
                pixels++;
                covered += pixel;
            }
            return false;
        }

        public Vector2 SnapToFloor(Vector2 point)
        {
            Vector2 newPoint = point;
            for (int i = 0; i < 30; i++)
            {
                Vector3Int cell = grid.WorldToCell(newPoint);
                if (map.HasTile(cell))
                {
                    newPoint.y += pixel;
                    continue;
                }
                else
                {
                    return newPoint;
                }
            }
            return Vector2.zero;
        }

        public Vector2 Pixelize(Vector2 point)
        {
            return new Vector2(NearestPixel(point.x), NearestPixel(point.y));
        }

        public float NearestPixel(float val)
        {
            return Mathf.Round(val * 8) / 8;
        }
    }
}