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
        public Vector3Int lastGroundedPosition;
        public Vector3Int lastFramePos;
        public int wallCastPixels = 7;
        public enum TileType { FullBrick, PartialBrick, LadderTopLeft, LadderTopRight, LadderLeft, LadderRight, RopeTop, Rope, Belt, Pole };
        public enum BeltType { None, Left, Right };

        public Vector3Int pos { get { return Vector3Int.RoundToInt(transform.position); } }

        public void EnvUpdate()
        {
            vecStatus.text = "Vector3" + pos.ToString();
            groundStatus.text = "IsGrounded: " + IsGrounded.ToString();
            string text = "null";
            MRTile tileData = GetTileAtPosition(pos);
            if (tileData != null)
            {
                text = tileData.name;
            }
            tileStatus.text = "Current Tile At Feet: " + text;
            string textBelow = "null";
            MRTile tileDataBelow = GetTileAtPosition(pos + Vector3Int.down);
            if (tileDataBelow != null)
            {
                textBelow = tileDataBelow.name;
            }
            belowTileStatus.text = "Below Feet: " + textBelow;
        }

        public MRTile GetTileAtPosition(Vector3Int pos)
        {
            Vector3Int cell = grid.WorldToCell(pos);
            if (!map.HasTile(cell)) return null;
            return map.GetTile<MRTile>(cell);
        }

        public MRTile TileAtFeet { get { return GetTileAtPosition(pos); } }
        public MRTile TileUnderFeet { get { return GetTileAtPosition(pos + Vector3Int.down); } }
        
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
                    if (insideTile && underTileData.solidity == MRTile.Solidity.Semisolid) return false;
                }
                return true;
            }
        }

        public bool IsOnLeftBelt
        {
            get
            {
                if (!IsGrounded) return false;
                MRTile mrTile = TileUnderFeet;
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
        
        public bool IsAboveRopeTop
        {
            get
            {
                MRTile mrTile = TileUnderFeet;
                if (mrTile == null) return false;
                if (mrTile.tileType == MRTile.TileType.RopeTop)
                    return true;
                else
                    return false;
            }
        }

        public bool IsOnRightBelt
        {
            get
            {
                if (!IsGrounded) return false;
                MRTile mrTile = TileUnderFeet;
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
                return IsBlocked(pos, Vector3Int.left, wallCastPixels);
            }
        }

        public bool RightBlocked
        {
            get
            {
                return IsBlocked(pos, Vector3Int.right, wallCastPixels);
            }
        }


        public bool IsBlocked(Vector3Int pos, Vector3Int dir, int distance)
        {
            int covered = 0;
            int pixels = 2;
            bool blocked = false;
            while (Mathf.Abs(covered) < distance && !blocked)
            {
                Vector3 newPoint = pos;
                newPoint.x += dir.x * pixels;
                Vector3Int cell = grid.WorldToCell(newPoint);
                if (map.HasTile(cell))
                {
                    MRTile mrTile = map.GetTile<MRTile>(cell);
                    if (mrTile.solidity == MRTile.Solidity.Solid)
                    {
                        Debug.Log("Blocked");
                        return true;
                    }
                }
                pixels++;
                covered++;
            }
            return false;
        }

        public int LadderSnap()
        {
            var cell = grid.WorldToCell(transform.position);
            var coords = grid.CellToWorld(cell);
            var at = TileAtFeet;
            var under = TileUnderFeet;
            bool snapRight = false;
            if (at != null)
            {
                if (at.tileType == MRTile.TileType.LadderBottomLeft) snapRight = true;
            }
            if (under != null && !snapRight)
            {
                if (under.tileType == MRTile.TileType.LadderTopLeft) snapRight = true;
            }

            int snapPoint = Mathf.RoundToInt(coords.x);
            if (snapRight)
            {
                snapPoint += 8;
            }
            return snapPoint;
        }

        public Vector3Int SnapToFloor(Vector3Int point)
        {
            // TODO
            return Vector3Int.zero;
        }
    }
}