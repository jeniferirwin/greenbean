using UnityEngine;
using UnityEngine.SceneManagement;

namespace Com.Technitaur.GreenBean.Core
{
    public class ReloadGame : MonoBehaviour
    {
        public void Start()
        {
            Lives.Amount = 5;
            SceneManager.LoadScene("Main");
        }
    }
}
