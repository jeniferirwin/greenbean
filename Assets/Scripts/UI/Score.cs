using UnityEngine;
using UnityEngine.UI;
using System;

namespace GreenBean.UI
{
    public class Score : MonoBehaviour
    {
        public Image[] slots;
        public Sprite[] sprites;
        public Sprite blank;
        
        public void SetText(int inputVal)
        {
            if (inputVal > 999999)
            {
                inputVal = 999999;
            }

            BlankScore();
            char[] valArray = inputVal.ToString().ToCharArray();
            Array.Reverse(valArray);
            int[] numbers = new int[valArray.Length];
            for (int i = 0; i < numbers.Length; i++)
            {
                int slotNumber = Int32.Parse(valArray[i].ToString());
                slots[i].sprite = sprites[slotNumber];
            }
        }
        
        public void BlankScore()
        {
            for (int i = 0; i < slots.Length; i++)
            {
                slots[i].sprite = blank;
            }
        }
        
        public void Start()
        {
            SetText(0);
        }
    }
}