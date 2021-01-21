using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

namespace Com.Technitaur.GreenBean.Inventory
{
    public class ScoreManager : MonoBehaviour
    {
        public const int MAX_SLOTS = 6;
        public const int NUM_SPRITES = 10;
        public const int MAX_SCORE = 999999;
        public const char PAD_CHAR = 'N';

        public Sprite[] Sprites = new Sprite[NUM_SPRITES];
        public GameObject[] Slots = new GameObject[MAX_SLOTS];
        public SpriteRenderer[] Renderers = new SpriteRenderer[MAX_SLOTS];

        public int Score;

        public void Start()
        {
            Inventory.Events.OnItemConsumed += AddScore;
            Inventory.Events.OnItemPickedUp += AddScore;
            for (int i = 0; i < Slots.Length; i++)
            {
                Renderers[i] = Slots[i].GetComponent<SpriteRenderer>();
            }
            UpdateScore();
        }
        
        public void AddScore(int worth)
        {
            Score += worth;
            UpdateScore();
        }

        public void UpdateScore()
        {
            SetSlots(IntToSprites(Score));
        }
        
        private void SetSlots(Sprite[] sprites)
        {
            for (int i = 0; i < MAX_SLOTS; i++)
            {
                Renderers[i].sprite = sprites[i];
            }
        }
        
        private Sprite[] IntToSprites(int number)
        {
            Sprite[] slotSprites = new Sprite[MAX_SLOTS];
            var numString = number.ToString().PadLeft(6,PAD_CHAR);
            for (int i = 0; i < MAX_SLOTS; i++)
            {
                if (numString[i].Equals(PAD_CHAR))
                {
                    slotSprites[i] = null;
                }
                else
                {
                    int num = (int) Char.GetNumericValue((char) numString[i]);
                    slotSprites[i] = Sprites[num];
                }
            }
            return slotSprites;
        }
    }
}
