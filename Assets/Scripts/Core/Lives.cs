using UnityEngine;

namespace Com.Technitaur.GreenBean.Core
{
    public static class Lives
    {
        public delegate void LivesChanged(int newAmount);
        public static event LivesChanged OnLivesChanged;

        private static int amount = 5;
        
        public static int Amount
        {
            get
            {
                return amount;
            }
            private set
            {
                amount = value;
            }
        }
        
        public static void Start()
        {
            UpdateLives();
        }
        
        public static void UpdateLives()
        {
            OnLivesChanged?.Invoke(Amount);
        }

        public static void Decrement()
        {
            Amount--;
            UpdateLives();
        }
        
        public static void Increment()
        {
            Amount++;
            UpdateLives();
        }
    }
}
