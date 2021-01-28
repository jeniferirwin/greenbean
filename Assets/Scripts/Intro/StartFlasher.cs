using UnityEngine;

namespace Com.Technitaur.GreenBean.Intro
{
    public class StartFlasher : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer rend = null;
        private int frame = 32;
        private static bool started = false;
        
        public void Start()
        {
            rend.enabled = false;    
        }
        
        public static void StartFlashing()
        {
            started = true;
        }
        
        public static void StopFlashing()
        {
            started = false;
        }

        public void FixedUpdate()
        {
            if (!started) return;
            if (rend.enabled == true && frame < 1)
            {
                rend.enabled = false;
                frame = 33;
            }
            else if (rend.enabled == false && frame < 1)
            {
                rend.enabled = true;
                frame = 32;
            }
            frame--;
        }
    }
}
