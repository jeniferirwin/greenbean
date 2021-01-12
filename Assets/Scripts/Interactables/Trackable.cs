using UnityEngine;

namespace Com.Technitaur.GreenBean.Interactables
{
    public class Trackable : MonoBehaviour
    {
        public bool IsDirty { get; private set; }

        [Header("Trackable")]
        [SerializeField] private Sprite dirtySprite;
        [SerializeField] private Sprite cleanSprite;
        [SerializeField] private SpriteRenderer rend;
        [SerializeField] private BoxCollider2D objCollider;
        
        public GameObject GetGameObject() => gameObject;
        
        public void SetDirty()
        {
            rend.sprite = dirtySprite;
            objCollider.gameObject.SetActive(false);
            IsDirty = true;
        }
        
        public void SetClean()
        {
            rend.sprite = cleanSprite;
            objCollider.gameObject.SetActive(true);
            IsDirty = false;
        }
    }
}
