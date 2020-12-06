using UnityEngine;

namespace Com.Technitaur.GreenBean.UI
{
    public class NumberSlot : MonoBehaviour
    {
        public SpriteRenderer spriteRenderer;
        public Texture2D texture;
        public Sprite blank;
        public int level;
        public int number;
        public Rect rect;
        public Vector2 pivot = new Vector2(0.5f, 0.5f);
        public int ppu = 1;

        public void Start()
        {
            spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            texture = spriteRenderer.sprite.texture;
            SetLevel(this.level);
        }
        
        public void SetNumber(int number)
        {
            if (number > 9) number = 9;
            if (number < 0)
            {
                spriteRenderer.sprite = blank;
                return;
            }
            int rectY = texture.height - 16 * Mathf.Max(1, level);
            int rectX = 16 * number;
            rect = new Rect(rectX, rectY, 16f, 16f);
            spriteRenderer.sprite = Sprite.Create(this.texture,this.rect,this.pivot,this.ppu,0,SpriteMeshType.Tight);
        }
        
        public void SetLevel(int level)
        {
            this.level = level;
            SetNumber(this.number);
        }
    }
}
