using UnityEngine;

namespace GreenBean.C64
{
    public class PixConvert
    {
        public static float PixelsToUnits(float pixels)
        {
            return pixels / 8f;
        }
        
        public static float PixelsToUnits(int pixels)
        {
            return (float) pixels / 8f;
        }
    }
}