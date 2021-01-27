using UnityEngine;

namespace Com.Technitaur.GreenBean.Player
{
    public class SmokeParticle : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer rend;
        [SerializeField] private Sprite[] sprites;
        
        private bool started;
        private int frames;

        public void Start()
        {
            rend.sprite = sprites[0];
            frames = 0;
        }
        
        public void FixedUpdate()
        {
            if (started) rend.sprite = sprites[1];
            else started = true;
            transform.position += Vector3.up;
            frames++;
            if (frames == 62)
            {
                GameObject.Destroy(this.gameObject);
            }
        }
    }
}
