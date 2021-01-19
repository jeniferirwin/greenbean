using UnityEngine;
using Com.Technitaur.GreenBean.Core;
using UnityEngine.Tilemaps;
using System;

namespace Com.Technitaur.GreenBean.Tilemaps
{
    public class Sensor : MonoBehaviour, ISensor
    {
        public Tilemap map;
        public Grid grid;
        [SerializeField] private Vector2 castDirection = Vector2.zero;
        [SerializeField] private LayerMask doorMask = 0;

        public bool IsNull { get; private set; }
        public bool AtLeftLadderTop { get; private set; }
        public bool AtRightLadderTop { get; private set; }
        public bool AtLeftLadder { get; private set; }
        public bool AtRightLadder { get; private set; }
        public bool AtRope { get; private set; }
        public bool AtSolid { get; private set; }
        public bool AtSemisolid { get; private set; }
        public bool AtPole { get; private set; }
        public bool AtLeftBelt { get; private set; }
        public bool AtRightBelt { get; private set; }
        public bool AtClosedDoor { get; private set; }

        public void Start()
        {
            map = GameObject.Find("8x8").GetComponent<Tilemap>();
            grid = GameObject.Find("Grid").GetComponent<Grid>();
        }
        public void SensorUpdate()
        {
            ResetVariables();
            TileBase tile = TileAtLoc();
            bool isDoorHere = DoorAtLoc();
            if (tile == null && !isDoorHere)
            {
                IsNull = true;
                return;
            }
            if (tile != null)
            {
                GetVariables(tile);
            }
            if (isDoorHere)
            {
                AtClosedDoor = true;
            }
        }

        public Vector2 CurrentTileCenter()
        {
            var loc = Vector2Int.RoundToInt(transform.position);
            var cell = grid.WorldToCell(transform.position);
            var cellLoc = grid.CellToWorld(cell);
            var cellCenter = cellLoc + new Vector3(4, 4, 0);
            return cellCenter;
        }

        private bool IsLocInMiddleOfTile() {
            var loc = Vector2Int.RoundToInt(transform.position);
            var cellCenter = CurrentTileCenter();
            var distance = Mathf.Abs(cellCenter.x - loc.x);
            if (distance < 2) return true;
            return false;
        }

        private bool DoorAtLoc()
        {
            var hit = Physics2D.CircleCast(transform.position, 1f, castDirection, 1f, doorMask);

            if (hit.collider == null)
            {
                return false;
            }
            bool isDoorClosed = hit.collider.gameObject.activeSelf;
            if (!isDoorClosed) return false;
            return true;
        }

        public TileBase TileAtLoc()
        {
            Vector3Int cell = grid.WorldToCell(transform.position);
            if (!map.HasTile(cell)) return null;
            return map.GetTile<TileBase>(cell);
        }

        public void GetVariables(TileBase tile)
        {
            if (tile is ICustomTile)
            {
                ICustomTile newTile = (ICustomTile) tile;
                AtLeftLadder = newTile.IsLeftLadder;
                AtRightLadder = newTile.IsRightLadder;
                AtLeftLadderTop = newTile.IsLeftLadder && newTile.IsSemisolid;
                AtRightLadderTop = newTile.IsRightLadder && newTile.IsSemisolid;
                AtRope = newTile.IsRope;
                AtSolid = newTile.IsSolid;
                AtSemisolid = newTile.IsSemisolid;
                AtPole = newTile.IsPole && IsLocInMiddleOfTile();
                AtLeftBelt = newTile.IsLeftBelt;
                AtRightBelt = newTile.IsRightBelt;
            }
            else
            {
                return;
            }
        }

        public void ResetVariables()
        {
            AtLeftLadder = false;
            AtRightLadder = false;
            AtLeftLadderTop = false;
            AtRightLadderTop = false;
            AtRope = false;
            AtSolid = false;
            AtSemisolid = false;
            AtPole = false;
            AtLeftBelt = false;
            AtRightBelt = false;
            AtClosedDoor = false;
            IsNull = false;
        }
    }
}
