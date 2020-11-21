using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;

namespace Com.Technitaur.GreenBean
{
    public class Environment : MonoBehaviour
    {
        public TMP_Text vecStatus;
        public TMP_Text groundStatus;
        public TMP_Text tileStatus;
        public TMP_Text moduloX;
        public TMP_Text moduloY;
        public TMP_Text belowTileStatus;

        public GridLayout grid;
        public Tilemap map;
        public Vector2 lastFramePos;
        public float pixel = 0.125f;
        public float wallCastPixels = 7;
        public Vector2 lastGroundedPosition;
        public enum TileType { FullBrick, PartialBrick, LadderTopLeft, LadderTopRight, LadderLeft, LadderRight, RopeTop, Rope, Belt, Pole };
        public enum BeltType { None, Left, Right };

        public Vector2 pos { get { return (Vector2)transform.position; } }
        public Vector2 UpPixel { get { return pos + Vector2.up * pixel; } }
        public Vector2 DownPixel { get { return pos + Vector2.down * pixel; } }
        public Vector2 LeftPixel { get { return pos + Vector2.left * pixel; } }
        public Vector2 RightPixel { get { return pos + Vector2.right * pixel; } }

        public void CurrentPosModulos()
        {
            float xmod = transform.position.x % 1;
            float ymod = transform.position.y % 1;
            moduloX.text = "Modulo X: " + xmod.ToString();
            moduloY.text = "Modulo Y: " + ymod.ToString();
        }

        public void EnvUpdate()
        {
            CurrentPosModulos();
            vecStatus.text = "Vector2" + pos.ToString();
            groundStatus.text = "IsGrounded: " + IsGrounded.ToString();
            string text = "null";
            MRTile tileData = GetTileAtPosition(pos);
            if (tileData != null)
            {
                text = tileData.name;
            }
            tileStatus.text = "Current Tile At Feet: " + text;
            string textBelow = "null";
            MRTile tileDataBelow = GetTileAtPosition(pos + Vector2.down * pixel);
            if (tileDataBelow != null)
            {
                textBelow = tileDataBelow.name;
            }
            belowTileStatus.text = "Below Feet: " + textBelow;
        }

        /*
        public Vector2 TileLookahead(Vector2 destination)
        {
            Vector2 seeking = transform.position;
            bool found = false;
            while (!found)
            {
                float xdist = destination.x - seeking.x;
                float ydist = destination.y - seeking.y;
                if (xdist < pixel && ydist < pixel)
                {
                    found = true;
                }
                if (destination.x - seeking.x > pixel)
                    seeking.x += pixel;
                if (destination.y - seeking.y > pixel)
                    seeking.y += pixel;    


                
            }
            
        }
        */

        public MRTile GetTileAtPosition(Vector2 pos)
        {
            Vector3Int cell = grid.WorldToCell(pos);
            if (!map.HasTile(cell)) return null;
            return map.GetTile<MRTile>(cell);
        }

        public MRTile TileAtFeet { get { return GetTileAtPosition(transform.position); } }
        public MRTile TileUnderFeet { get { return GetTileAtPosition((Vector2) transform.position + Vector2.down * pixel); } }
        
        public bool IsAtUpperPixel
        {
            get
            {
                float mod = Mathf.Abs(transform.position.y) % 1;
                if (mod > pixel * 7) return true;
                return false;
            }
        }

        public bool IsGrounded
        {
            get
            {
                MRTile underTileData = TileUnderFeet;
                MRTile atTileData = TileAtFeet;

                if (underTileData == null) return false;

                if (underTileData.solidity == MRTile.Solidity.None) return false;
                
                if (atTileData != null)
                {
                    bool insideTile = atTileData.GetInstanceID() == underTileData.GetInstanceID();
                    if (insideTile && underTileData.solidity == MRTile.Solidity.Semisolid && !IsAtUpperPixel) return false;
                }
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

        public bool IsAtLadder
        {
            get
            {
                MRTile mrTile = TileAtFeet;
                if (mrTile == null) return false;
                switch (mrTile.tileType)
                {
                    case MRTile.TileType.LadderBottomLeft:
                    case MRTile.TileType.LadderBottomRight:
                        return true;
                    default:
                        return false;
                }
            }
        }

        public bool IsAboveLadder
        {
            get
            {
                MRTile mrTile = TileUnderFeet;
                if (mrTile == null) return false;
                switch (mrTile.tileType)
                {
                    case MRTile.TileType.LadderTopLeft:
                    case MRTile.TileType.LadderTopRight:
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