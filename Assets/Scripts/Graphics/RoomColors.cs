using UnityEngine;

namespace Com.Technitaur.GreenBean.Graphics
{
    public class RoomColors : MonoBehaviour
    {
        [SerializeField] private int level = 1;

        private void Start() => FindObjectOfType<TexDB>().RecolorAll(level);
    }
}
