using UnityEngine;

namespace Com.Technitaur.GreenBean.Interactables
{
    public abstract class Trackable : MonoBehaviour
    {
        public bool IsDirty { get; protected set; }
        public Sprite CleanSprite { get { return cleanSprite; } private set { cleanSprite = value; } }

        [Header("Trackable")]
        [SerializeField] protected Tracker tracker;
        [SerializeField] protected Sprite dirtySprite = null;
        [SerializeField] protected Sprite cleanSprite = null;
        [SerializeField] protected SpriteRenderer rend = null;
        [SerializeField] protected BoxCollider2D objCollider = null;
        [SerializeField] protected Vector2Int startPosition;
        
        public virtual GameObject GetGameObject() => gameObject;
        
        public virtual void OnEnable()
        {
            startPosition = Vector2Int.RoundToInt(transform.position);
            tracker = GameObject.FindObjectOfType<Tracker>();
            if (tracker == null) Debug.Log("WTF?");
            tracker.AddToList(gameObject, startPosition);
            if (tracker.IsObjectDirty(gameObject, startPosition)) SetDirty();
            else SetClean();
        }

        public virtual void SetDirty()
        {
            rend.sprite = dirtySprite;
            objCollider.gameObject.SetActive(false);
            IsDirty = true;
            tracker.SetObjectDirty(gameObject, startPosition);
        }
        
        public virtual void SetClean()
        {
            rend.sprite = cleanSprite;
            objCollider.gameObject.SetActive(true);
            IsDirty = false;
        }
    }
}
