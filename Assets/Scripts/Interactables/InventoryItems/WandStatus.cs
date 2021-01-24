using UnityEngine;
using Com.Technitaur.GreenBean.Core;

namespace Com.Technitaur.GreenBean.Interactables
{
    public class WandStatus : MonoBehaviour, IWandStatus
    {
        [SerializeField] private GameObject _buff = null;
        
        public bool IsBuffActive { get { return _buff.activeSelf; } }
        
        public void ActivateBuff()
        {
            _buff.SetActive(true);
        }
    }
}
