using UnityEngine;

namespace Com.Technitaur.GreenBean.Interactables
{
    public class Kicker : MonoBehaviour
    {
        private void Start()
        {
            var tracker = GameObject.FindObjectOfType<Tracker>();
            tracker.CheckScene();
            gameObject.SetActive(false);
        }
    }
}
