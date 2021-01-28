using UnityEngine;

namespace Com.Technitaur.GreenBean.Intro
{
    public class FakeHolo : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer rend;

        public void Hide()
        {
            rend.sprite = null;
        }
    }
}
