using UnityEngine;
using Com.Technitaur.GreenBean.Core;

namespace Com.Technitaur.GreenBean.Interactables
{
    public class WandBuff : MonoBehaviour
    {
        private IInventory _inv;

        private float timer;
        
        private void Start()
        {
            _inv = GameObject.Find("Inventory").GetComponent<IInventory>();
            gameObject.SetActive(false);
        }
        
        private void OnEnable()
        {
            timer = 4f;
            FadeMobs(true);
        }

        private void OnDisable()
        {
            FadeMobs(false);
            _inv.Consume(ItemType.Wand);
        }
        
        private void Update()
        {
            if (timer > 0f)
            {
                timer -= Time.deltaTime;
                return;
            }
            gameObject.SetActive(false);
        }
        
        private void FadeMobs(bool fading)
        {
            var mobs = GameObject.FindObjectsOfType<Mob>();
            foreach (var mob in mobs)
            {
                mob.Fade(fading);
            }
        }
    }
}
