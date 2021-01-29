using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
using Com.Technitaur.GreenBean.Core;

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
        public int scorePerExtraLife = 10000;

        public int Score;
        private int scoreToNextLife = 0;

        public void Start()
        {
            Inventory.Events.OnItemConsumed += AddScore;
            Inventory.Events.OnItemPickedUp += AddScore;
            for (int i = 0; i < Slots.Length; i++)
            {
                Renderers[i] = Slots[i].GetComponent<SpriteRenderer>();
            }
            scoreToNextLife = scorePerExtraLife;
            UpdateScore();
        }
        
        public void OnDestroy()
        {
            Inventory.Events.OnItemConsumed -= AddScore;
            Inventory.Events.OnItemPickedUp -= AddScore;
        }
        
        public void AddScore(ItemType itemType, int worth)
        {
            Score += worth;
            UpdateScore();
            scoreToNextLife -= worth;
            Debug.Log($"Next life in {scoreToNextLife} points.");
            if (scoreToNextLife <= 0)
            {
                scoreToNextLife = scorePerExtraLife - Mathf.Abs(scoreToNextLife);
                Debug.Log($"Next life in {scoreToNextLife} points.");
                AudioManager.StopOneShot();
                AudioManager.EmitOnce(AudioManager.Sound.GainLife);
                Lives.Increment();
            }
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
