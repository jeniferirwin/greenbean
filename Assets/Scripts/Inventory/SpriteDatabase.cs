using UnityEngine;

namespace Com.Technitaur.GreenBean.Inventory
{
    [CreateAssetMenu(menuName="Sprite Database", order=1)]
    public class SpriteDatabase : ScriptableObject
    {
        public Sprite[] none;
        public Sprite[] sword;
        public Sprite[] redKey;
        public Sprite[] cyanKey;
        public Sprite[] violetKey;
        public Sprite[] coin;
        public Sprite[] wand;
        public Sprite[] torch;
    }
}
