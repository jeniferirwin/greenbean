using UnityEngine;
using UnityEngine.SceneManagement;

namespace Com.Technitaur.GreenBean.Core
{
    public class EndingScreenStarter : MonoBehaviour
    {
        public void Awake()
        {
            SceneManager.LoadScene("Ending");
        }
    }
}
