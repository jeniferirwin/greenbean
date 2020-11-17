using UnityEngine;

namespace GreenBean.UI
{
    public class Lives : MonoBehaviour
    {
        public GameObject[] slots;
        
        public void SetLives(int number)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                slots[i].SetActive(false);
            }
            
            for (int i = 0; i < number; i++)
            {
                slots[i].SetActive(true);
            }
        }
        
        public void Start()
        {
            SetLives(2);
        }
    }
}