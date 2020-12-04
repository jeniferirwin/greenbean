namespace Com.Technitaur.GreenBean.Core
{
    public static class Tables
    {
        public struct Item
        {
            public int ID;
            public int Score;
            public ItemType Type;
        }

        public enum ItemType { None, Coin, RedKey, VioletKey, CyanKey, Sword, Torch, Wand };
        public enum Door { Red, Violet, Cyan };
        public enum Enemy { RollingSkull, BouncingSkull, Snake, Spider };
    }
}
