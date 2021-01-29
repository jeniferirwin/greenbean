using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Com.Technitaur.GreenBean.Core
{
    public class GameOverController : MonoBehaviour
    {
        [SerializeField] public SpriteRenderer rend;

        public void RestartGame(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                SceneManager.LoadScene("Main");
            }
        }
    }
}
