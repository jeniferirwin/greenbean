using UnityEngine;

namespace Com.Technitaur.GreenBean.Interactables
{
    public abstract class Trackable : MonoBehaviour
    {
        public bool IsDirty { get; private set; }
        public Sprite CleanSprite { get { return cleanSprite; } private set { cleanSprite = value; } }

        [Header("Trackable")]
        [SerializeField] private Tracker tracker;
        [SerializeField] private Sprite dirtySprite = null;
        [SerializeField] private Sprite cleanSprite = null;
        [SerializeField] private SpriteRenderer rend = null;
        [SerializeField] private BoxCollider2D objCollider = null;
        
        public virtual GameObject GetGameObject() => gameObject;
        
        public virtual void OnEnable()
        {
            tracker = GameObject.FindObjectOfType<Tracker>();
            tracker.AddToList(gameObject);
            if (tracker.IsObjectDirty(gameObject)) SetDirty();
            else SetClean();
        }

        public virtual void SetDirty()
        {
            rend.sprite = dirtySprite;
            objCollider.gameObject.SetActive(false);
            IsDirty = true;
            tracker.SetObjectDirty(gameObject);
        }
        
        public virtual void SetClean()
        {
            rend.sprite = cleanSprite;
            objCollider.gameObject.SetActive(true);
            IsDirty = false;
        }
    }
}
