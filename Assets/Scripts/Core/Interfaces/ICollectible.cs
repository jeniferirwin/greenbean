using UnityEngine;

namespace Com.Technitaur.GreenBean.Core
{
    public enum ItemType
    {
        None,
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
        ItemType Item { get; }
        int VanishTimer { get; }
    }
}
