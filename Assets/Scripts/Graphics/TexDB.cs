using UnityEngine;

namespace Com.Technitaur.GreenBean.Graphics
{
    public class TexDB : MonoBehaviour
    {
        [SerializeField] private Texture2D[] baseTextures = null;
        [SerializeField] private Texture2D[] liveTextures = null;

        public void RecolorAll(int level)
        {
            for (int i = 0; i < baseTextures.Length; i++)
            {
                ColorSchemes.RecolorTexture(ref liveTextures[i], baseTextures[i], level);
            }
        }
    }
}
