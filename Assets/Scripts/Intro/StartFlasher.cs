using UnityEngine;

namespace Com.Technitaur.GreenBean.Intro
{
    public class StartFlasher : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer rend = null;
        private int frame = 32;
        private static bool started = false;
        private static bool finished = false;
        
        public void Start()
        {
            rend.enabled = false;    
        }
        
        public void OnDestroy()
        {
            started = false;
            rend.enabled = false;
            finished = false;
            frame = 32;
        }
        
        public static void StartFlashing()
        {
            started = true;
        }
        
        public static void StopFlashing()
        {
            started = false;
            finished = true;
        }

        public void FixedUpdate()
        {
            if (finished && rend.enabled == false) rend.enabled = true;
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
