using UnityEngine;
using Com.Technitaur.GreenBean.Core;
using UnityEngine.Tilemaps;

namespace Com.Technitaur.GreenBean.Tilemaps
{
    public class Sensor : MonoBehaviour, ISensor
    {
        public Tilemap map;
        public Grid grid;

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

        public void SensorUpdate()
        {
            ResetVariables();
            TileBase tile = TileAtLoc();
            GetVariables(tile);
        }

        public TileBase TileAtLoc()
        {
            Vector3Int cell = grid.WorldToCell(transform.position);
            if (!map.HasTile(cell)) return null;
            return map.GetTile<TileBase>(cell);
        }

        public void GetVariables(TileBase tile)
        {
            if (tile == null)
            {
                IsNull = true;
                return;
            }
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
                AtPole = newTile.IsPole;
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
        }
    }
}
