using UnityEngine;
using UnityEngine.SceneManagement;

namespace Com.Technitaur.GreenBean.Core
{
    public static class GameStatus
    {
        public delegate void PlayerDied();
        public static event PlayerDied OnPlayerDied;
        
        public static bool gameIsOver;

        public static void DeclareDead()
        {
            OnPlayerDied?.Invoke();
        }
        
        public static void GameOver()
        {
            gameIsOver = true;
            GameObject gameOverController = GameObject.Find("GameOverController");
            gameOverController.GetComponentInChildren<SpriteRenderer>(true).enabled = true;
        }
        
        public static void ReloadGame()
        {
            SceneManager.LoadScene("Between");
            gameIsOver = false;
        }
    }
}
