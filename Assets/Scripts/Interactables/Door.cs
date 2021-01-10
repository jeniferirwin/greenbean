using UnityEngine;
using Com.Technitaur.GreenBean.Player;
using Com.Technitaur.GreenBean.Core;

namespace Com.Technitaur.GreenBean.Interactables
{
    public class Door : MonoBehaviour
    {
        public bool IsOpen { get; private set; }

        [SerializeField] private Sprite openSprite;
        [SerializeField] private Sprite closedSprite;
        [SerializeField] private SpriteRenderer rend;
        [SerializeField] private Types.KeyType keyColor;
        [SerializeField] private BoxCollider2D doorCollider;
        
        public void Start()
        {
        }

        public void Open()
        {
            rend.sprite = openSprite;
            doorCollider.gameObject.SetActive(false);
        }
    }
}
