using UnityEngine;

namespace Com.Technitaur.GreenBean.Core
{
    public class SpawnPointHandler : MonoBehaviour
    {
        public Vector2Int NextSpawn { get; private set; }
        
        public void SetNextSpawn(Vector2 loc)
        {
            var rounded = Vector2Int.RoundToInt(loc);
            var sane = SanitizeSpawnPoint(rounded);
            NextSpawn = sane;
        }
        
        public void SetNextSpawn(Vector2Int loc)
        {
            var sane = SanitizeSpawnPoint(loc);
            NextSpawn = sane;
        }
        
        public Vector2Int SanitizeSpawnPoint(Vector2Int loc)
        {
            Vector2Int sanitized = loc;
            if (loc.y < -100) sanitized.y = -100;
            if (loc.y > 100) sanitized.y = 100;
            if (loc.x > 160) sanitized.x = 160;
            if (loc.x < -160) sanitized.x = -160;
            return sanitized;
        }
    }
}
