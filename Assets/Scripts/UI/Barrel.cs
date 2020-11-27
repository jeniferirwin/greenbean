using UnityEngine;

namespace Com.Technitaur.GreenBean.UI
{
    public class Barrel : MonoBehaviour
    {
        public SpriteRenderer spriteRenderer;
        public Texture2D texture;
        public int level;

        public void Start()
        {
            this.texture = spriteRenderer.sprite.texture;
            SetLevel(level);
        }

        public void SetSprite(int level)
        {
            Vector2 pivot = new Vector2(0.5f, 0.5f);
            int ppu = 1;
            int rectY = texture.height - Mathf.Max((level * 16),16);
            int rectX = 0;
            Rect rect = new Rect(rectX, rectY, 16, 16);
            spriteRenderer.sprite = Sprite.Create(this.texture, rect, pivot, ppu, 0, SpriteMeshType.Tight);
        }

        public void SetLevel(int number)
        {
            this.level = number;
            SetSprite(this.level);
        }
    }
}
