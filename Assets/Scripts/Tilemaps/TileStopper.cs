using UnityEngine;
using Com.Technitaur.GreenBean.Core;
using UnityEngine.Tilemaps;

namespace Com.Technitaur.GreenBean.Tilemaps
{
    public class TileStopper : MonoBehaviour
    {
        public static Tilemap[] maps;

        public void Start()
        {
            GameStatus.OnPlayerDied += StopTiles;
        }
        
        public static void StartTiles()
        {
            maps = GameObject.FindObjectsOfType<Tilemap>();
            foreach (var map in maps)
            {
                map.animationFrameRate = 0.5f;
            }
        }

        public static void StopTiles()
        {
            maps = GameObject.FindObjectsOfType<Tilemap>();
            foreach (var map in maps)
            {
                map.animationFrameRate = 0;
            }
        }
    }
}
