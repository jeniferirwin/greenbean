using UnityEngine;

namespace Com.Technitaur.GreenBean.Intro
{
    public class FakeHolo : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer rend = null;

        public void Hide()
        {
            rend.sprite = null;
        }
    }
}
