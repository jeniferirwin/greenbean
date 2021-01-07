using UnityEngine;
using UnityEngine.Experimental.Rendering;

namespace Com.Technitaur.GreenBean.Graphics
{
    public static class PaletteSwap
    {
        public static void ColorSwap(SpriteRenderer renderer, int level)
        {
            var block = new MaterialPropertyBlock();
            var tex = renderer.sprite.texture;
            var schemes = VICEPalette.schemes;
            block.SetTexture("_MainTex",RecolorBaseTexture(tex, schemes[level]));
            renderer.SetPropertyBlock(block);
        }
        
        public static Texture2D RecolorBaseTexture(Texture2D texture, Color[] scheme)
        {
            Color[] pixels = texture.GetPixels(0, 0, texture.width, texture.height);
            for (int i = 0; i < pixels.Length; i++)
            {
                if (pixels[i].r == 1.0f && pixels[i].a > 0.5f)
                {
                    pixels[i] = scheme[0];
                }
                else if (pixels[i].g == 1.0f && pixels[i].a > 0.5f)
                {
                    pixels[i] = scheme[1];
                }
            }
            Texture2D newTex = new Texture2D(texture.width, texture.height);
            newTex.filterMode = FilterMode.Point;
            newTex.SetPixels(0,0,texture.width,texture.height,pixels);
            newTex.Apply();
            return newTex;
        }
    }
}
