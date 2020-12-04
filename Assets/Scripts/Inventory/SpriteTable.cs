using UnityEngine;

namespace Com.Technitaur.GreenBean.Inventory
{
    [CreateAssetMenu(fileName = "SpriteTable", menuName = "Sprite Table", order = 1)]
    public class SpriteTable : ScriptableObject
    {
        public Sprite[] cyanKey;
        public Sprite[] violetkey;
        public Sprite[] redKey;
        public Sprite[] wand;
        public Sprite[] sword;
        public Sprite[] coin;
        public Sprite[] torch;
    }
}
