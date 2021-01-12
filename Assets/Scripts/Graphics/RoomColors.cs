using UnityEngine;

namespace Com.Technitaur.GreenBean.Graphics
{
    public class RoomColors : MonoBehaviour
    {
        public Texture2D liveTexture;

        [SerializeField] private Texture2D baseTexture = null;
        [SerializeField] private int level = 1;
        
        private void Awake()
        {
            ColorSchemes.RecolorTexture(ref liveTexture, baseTexture, level);
        }
    }
}
