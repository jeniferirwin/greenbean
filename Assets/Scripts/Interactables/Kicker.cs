using UnityEngine;

namespace Com.Technitaur.GreenBean.Interactables
{
    public class Kicker : MonoBehaviour
    {
        private void Start()
        {
            var tracker = GameObject.FindObjectOfType<Tracker>();
            gameObject.SetActive(false);
        }
    }
}
