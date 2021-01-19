using UnityEngine;

namespace Com.Technitaur.GreenBean.Interactables
{
    public class Trackable : MonoBehaviour
    {
        public bool IsDirty { get; private set; }
        public Sprite CleanSprite { get { return cleanSprite; } private set { cleanSprite = value; } }

        [Header("Trackable")]
        [SerializeField] private Tracker tracker;
        [SerializeField] private Sprite dirtySprite = null;
        [SerializeField] private Sprite cleanSprite = null;
        [SerializeField] private SpriteRenderer rend = null;
        [SerializeField] private BoxCollider2D objCollider = null;
        
        public GameObject GetGameObject() => gameObject;
        
        public void OnEnable()
        {
            tracker = GameObject.FindObjectOfType<Tracker>();
            tracker.AddToList(gameObject);
            if (tracker.IsObjectDirty(gameObject)) SetDirty();
            else SetClean();
        }

        public void SetDirty()
        {
            rend.sprite = dirtySprite;
            objCollider.gameObject.SetActive(false);
            IsDirty = true;
            tracker.SetObjectDirty(gameObject);
        }
        
        public void SetClean()
        {
            rend.sprite = cleanSprite;
            objCollider.gameObject.SetActive(true);
            IsDirty = false;
        }
    }
}
