using UnityEngine;

namespace Com.Technitaur.GreenBean.Graphics
{
    public class RoomColors : MonoBehaviour
    {
        [SerializeField] private int level;

        private void Start() => FindObjectOfType<TexDB>().RecolorAll(level);
    }
}
