using UnityEngine;
using Com.Technitaur.GreenBean.Core;
using UnityEngine.Tilemaps;
using TMPro;

namespace Com.Technitaur.GreenBean.Tilemaps
{
    public class Sensor : MonoBehaviour, ISensor
    {

        public TMP_Text text;
        public Tilemap map;
        public Grid grid;
        
        public bool AtLadder { get; private set; }
        public bool AtRope { get; private set; }
        public bool AtSolid { get; private set; }
        public bool AtSemiSolid { get; private set; }
        public bool AtHazard { get; private set; }
        public bool AtCollectible { get; private set; }
        public bool AtPole { get; private set; }
        public bool AtClosedDoor { get; private set; }
        
        public void Update()
        {
            CustomTile tile = TileAtLoc();
            if (tile == null)
            {
                text.text = "Null";
                return;
            }
            text.text = tile.name;
        }

        public CustomTile TileAtLoc()
        {
            Vector3Int cell = grid.WorldToCell(transform.position);
            if (!map.HasTile(cell)) return null;
            return map.GetTile<CustomTile>(cell);
        }
        
        public void GetVariables(CustomTile tile)
        {
            AtLadder = tile.isLadder;
            AtRope = tile.isRope;
            AtSolid = tile.isSolid;
            AtSemiSolid = tile.isSemiSolid;
            AtHazard = tile.isHazard;
            AtCollectible = tile.isCollectible;
            AtPole = tile.isPole;
            AtClosedDoor = (tile.isDoor && tile.isClosed);
        }
        
        public void ResetVariables()
        {
            AtLadder = false;
            AtRope = false;
            AtSolid = false;
            AtSemiSolid = false;
            AtHazard = false;
            AtCollectible = false;
            AtPole = false;
            AtClosedDoor = false;
        }
    }
}
