using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEditor;
using System.IO;

namespace Com.Technitaur.GreenBean.ReTex
{
    public static class ReTex
    {
        private static Color[] ColorSwap(Texture2D texture, int level)
        {
            Color[] pixels = texture.GetPixels(0);
            for (int i = 0; i < pixels.Length; i++)
            {
                if (pixels[i].a < 1f) continue;
                
                if (pixels[i] == Color.red)
                {
                    pixels[i] = VICEPalette.schemes[level][0];
                    Debug.Log(VICEPalette.schemes[level][0]);
                    continue;
                }

                if (pixels[i] == Color.green)
                {
                    pixels[i] = VICEPalette.schemes[level][1];
                    Debug.Log(VICEPalette.schemes[level][0]);
                    continue;
                }
            }
            return pixels;
        }
    }
}
