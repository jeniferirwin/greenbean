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
        public float wallCastPixels = 7;
        public Vector2 lastGroundedPosition;
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
                if (!map.HasTile(cell)) return false;
                MRTile tileData = map.GetTile<MRTile>(cell);

                if (tileData.solidity == MRTile.Solidity.None) return false;
                
                float upperPixel = cell.y + 6 * pixel;
                if (pos.y < upperPixel) return false;

                return true;
            }
        }
        
        public bool IsOnLeftBelt
        {
            get
            {
                if (!IsGrounded) return false;
                Vector2 point = DownPixel;
                Vector3Int cell = grid.WorldToCell(point);
                MRTile mrTile = map.GetTile<MRTile>(cell);
                switch (mrTile.tileType)
                {
                    case MRTile.TileType.LeftBeltLeft:
                    case MRTile.TileType.LeftBeltRight:
                    case MRTile.TileType.LeftBeltMiddle:
                        return true;
                    default:
                        return false;
                }
            }
        }

        public bool IsOnRightBelt
        {
            get
            {
                if (!IsGrounded) return false;
                Vector2 point = DownPixel;
                Vector3Int cell = grid.WorldToCell(point);
                MRTile mrTile = map.GetTile<MRTile>(cell);
                switch (mrTile.tileType)
                {
                    case MRTile.TileType.RightBeltLeft:
                    case MRTile.TileType.RightBeltRight:
                    case MRTile.TileType.RightBeltMiddle:
                        return true;
                    default:
                        return false;
                }
            }
        }
        
        public bool HasSolidTile(Vector3Int cell)
        {
            if (!map.HasTile(cell)) return false;
            MRTile mrTile = map.GetTile<MRTile>(cell);
            if (mrTile.solidity == MRTile.Solidity.Solid)
                return true;
            else
                return false;

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
            int pixels = 2;
            bool blocked = false;
            while (Mathf.Abs(covered) < distance && !blocked)
            {
                Vector2 newPoint = pos;
                newPoint.x += dir.x * pixel * pixels;
                Vector3Int cell = grid.WorldToCell(newPoint);
                if (map.HasTile(cell))
                {
                    MRTile mrTile = map.GetTile<MRTile>(cell);
                    if (mrTile.solidity == MRTile.Solidity.Solid)
                        return true;
                }
                pixels++;
                covered += pixel;
            }
            return false;
        }

        public Vector2 SnapToFloor(Vector2 point)
        {
            if (Mathf.Abs(point.y % 1) <= (float.Epsilon * 100)) return point;
            float ysnap = Mathf.Ceil(point.y);
            Vector2 newPos = new Vector2(point.x, ysnap);
            Debug.Log(newPos);
            return newPos;
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