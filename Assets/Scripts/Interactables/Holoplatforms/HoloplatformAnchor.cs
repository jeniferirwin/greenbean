using UnityEngine;
using Com.Technitaur.GreenBean.Core;

namespace Com.Technitaur.GreenBean.Interactables
{
    public class HoloplatformAnchor : MonoBehaviour
    {
        public delegate void HoloplatformLoaded();
        public static event HoloplatformLoaded OnHoloplatformLoaded;

        [SerializeField] private int length;
        
        private SpriteRenderer[] spriteSlots = null;
        private BoxCollider2D coll;
        private HoloSpriteDB db;
        
        private void Start()
        {
            BeSureToLikeAndSubscribe();
            var pos = Vector2Int.RoundToInt(transform.position);
            var masks = FindObjectOfType<LayerMaskDB>();
            db = FindObjectOfType<HoloSpriteDB>();
            HoloplatformGenerator.Generate(gameObject, length, masks.holoplatforms);
            spriteSlots = gameObject.GetComponentsInChildren<SpriteRenderer>();
            coll = gameObject.GetComponentInChildren<BoxCollider2D>(includeInactive:true);
            OnHoloplatformLoaded?.Invoke();
        }
        
        private void BeSureToLikeAndSubscribe()
        {
            PersistentCycleKeeper.OnFrameChanged += ShowFrame;
            PersistentCycleKeeper.OnHoloplatformsActive += TurnOn;
            PersistentCycleKeeper.OnHoloplatformsInactive += TurnOff;
        }
        
        private void OnDestroy()
        {
            PersistentCycleKeeper.OnFrameChanged -= ShowFrame;
            PersistentCycleKeeper.OnHoloplatformsActive -= TurnOn;
            PersistentCycleKeeper.OnHoloplatformsInactive -= TurnOff;
        }
        
        private void ShowFrame(int frame)
        {
            foreach (var slot in spriteSlots)
            {
                slot.sprite = db.holoFrames[frame];
            }
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
