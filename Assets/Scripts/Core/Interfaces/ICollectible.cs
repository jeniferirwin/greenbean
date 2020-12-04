using UnityEngine;

namespace Com.Technitaur.GreenBean.Core
{
    public enum ItemType
    {
        RedKey,
        CyanKey,
        VioletKey,
        Wand,
        Sword,
        Coin,
        Torch    
    }

    public interface ICollectible
    {
        ItemType ItemType { get; }
    }
}
