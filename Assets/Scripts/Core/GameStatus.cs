using UnityEngine;

namespace Com.Technitaur.GreenBean.Core
{
    public static class GameStatus
    {
        public delegate void PlayerDied();
        public static event PlayerDied OnPlayerDied;

        public static void DeclareDead()
        {
            OnPlayerDied?.Invoke();
        }
    }
}
