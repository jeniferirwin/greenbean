using UnityEngine;
using UnityEngine.UI;
using System;

namespace GreenBean.UI
{
    public class Score : MonoBehaviour
    {
        public int value;
        
        public Image[] slots;
        public Sprite[] sprites;
        public Sprite blank;
        
        private float debugTimerLength = 0.15f;
        private float currentDebugTimer;
        private int currentDebugScore;
        
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
            currentDebugScore = 0;
            SetText(currentDebugScore);
            currentDebugTimer = debugTimerLength;
        }
        
        public void Update()
        {
            if (currentDebugTimer <= 0)
            {
                currentDebugTimer = debugTimerLength;
                currentDebugScore += 5;
                SetText(currentDebugScore);
            }
            else
            {
                currentDebugTimer -= Time.deltaTime;
            }
        }
    }
}