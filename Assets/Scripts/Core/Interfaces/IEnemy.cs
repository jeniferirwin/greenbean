using UnityEngine;

namespace Com.Technitaur.GreenBean.Core
{
    public interface IEnemy
    {
        bool CanDie { get; }
        Vector3Int SpawnPoint { get; }
    }
}
