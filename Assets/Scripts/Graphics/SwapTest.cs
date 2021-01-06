using UnityEngine;

namespace Com.Technitaur.GreenBean.Graphics
{
    public class SwapTest : MonoBehaviour
    {
        public SpriteRenderer rend;

        public void Start()
        {
            var block = new MaterialPropertyBlock();
            block.SetTexture("_MainTex",PaletteSwap.TexSwap(rend.sprite.texture, VICEPalette.schemes[1]));
            rend.SetPropertyBlock(block);
        }
    }
}
