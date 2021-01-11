using UnityEngine;

namespace Com.Technitaur.GreenBean.Graphics
{
    public class RoomColors : MonoBehaviour
    {
        public Texture2D liveTexture;

        [SerializeField] private Texture2D baseTexture;
        [SerializeField] private int level;
        
        private void Awake()
        {
            ColorSchemes.RecolorTexture(ref liveTexture, baseTexture, level);
        }
    }
}
