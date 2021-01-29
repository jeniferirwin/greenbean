using UnityEngine;
using Com.Technitaur.GreenBean.Core;

namespace Com.Technitaur.GreenBean.Inventory
{
    public class LifeSlot : MonoBehaviour
    {
        [SerializeField] public int id;
        [SerializeField] public Sprite activeSprite;
        [SerializeField] public SpriteRenderer rend;
        
        private void Awake()
        {
            rend = GetComponent<SpriteRenderer>();
            activeSprite = rend.sprite;
            Lives.OnLivesChanged += CheckSlot;
        }
        
        private void OnDestroy()
        {
            Lives.OnLivesChanged -= CheckSlot;
        }
        
        private void Start()
        {
            CheckSlot(Lives.Amount);
        }
        
        private void CheckSlot(int amount)
        {
            if (id < amount) ActivateSlot();
            else DeactivateSlot();    
        }
        
        private void ActivateSlot()
        {
            rend.sprite = activeSprite;
        }

        private void DeactivateSlot()
        {
            rend.sprite = null;        
        }
    }
}
