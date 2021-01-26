using UnityEngine;
using Com.Technitaur.GreenBean.Core;

namespace Com.Technitaur.GreenBean.Interactables
{
    public class KillchainAnchor : MonoBehaviour
    {
        public delegate void KillchainLoaded();
        public static event KillchainLoaded OnKillchainLoaded;

        [SerializeField] private SpriteRenderer rend = null;
        [SerializeField] private BoxCollider2D coll;

        private KillchainSpriteDB db;
        
        private void Start()
        {
            BeSureToLikeAndSubscribe();
            db = FindObjectOfType<KillchainSpriteDB>();
            OnKillchainLoaded?.Invoke();
        }
        
        private void BeSureToLikeAndSubscribe()
        {
            PersistentCycleKeeper.OnFrameChanged += ShowFrame;
            PersistentCycleKeeper.OnKillchainsActive += TurnOn;
            PersistentCycleKeeper.OnKillchainsInactive += TurnOff;
        }
        
        private void OnDestroy()
        {
            PersistentCycleKeeper.OnFrameChanged -= ShowFrame;
            PersistentCycleKeeper.OnKillchainsActive -= TurnOn;
            PersistentCycleKeeper.OnKillchainsInactive -= TurnOff;
        }
        
        private void ShowFrame(int frame)
        {
            rend.sprite = db.chainFrames[frame];
        }
        
        private void TurnOff()
        {
            // if this doesn't work, we'll put it on another layer
            coll.enabled = false;
        }
        
        private void TurnOn()
        {
            coll.enabled = true;
        }
    }
}
