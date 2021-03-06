﻿using UnityEngine;
using Com.Technitaur.GreenBean.Core;

namespace Com.Technitaur.GreenBean.Interactables
{
    public class PersistentCycleKeeper : MonoBehaviour
    {
        public delegate void FrameChanged(int value);
        public static event FrameChanged OnFrameChanged;
        
        public delegate void HoloplatformsActive();
        public delegate void HoloplatformsInactive();
        
        public delegate void KillchainsActive();
        public delegate void KillchainsInactive();
        
        public static event HoloplatformsActive OnHoloplatformsActive;
        public static event HoloplatformsActive OnHoloplatformsInactive;
        
        public static event KillchainsActive OnKillchainsActive;
        public static event KillchainsInactive OnKillchainsInactive;

        public static int frame;
        
        public int Frame
        {
            get { return frame; }
            set
            {
                if (value > 105)
                {
                    frame = 0;
                }
                else
                {
                    frame = value;
                }
                switch (frame)
                {
                    case 0:
                        OnHoloplatformsActive?.Invoke();
                        break;
                    case 40:
                        OnKillchainsActive?.Invoke();
                        break;
                    case 93:
                        OnKillchainsInactive?.Invoke();
                        OnHoloplatformsInactive?.Invoke();
                        break;
                }
                OnFrameChanged?.Invoke(frame);                
            }
        }

        private void Start()
        {
            Frame = 99; // this is for the intro screen
            PauseCycle();
        }
        
        private void OnDestroy()
        {
            Frame = 99;
            HoloplatformAnchor.OnHoloplatformLoaded -= StartCycle;
            KillchainAnchor.OnKillchainLoaded -= StartCycle;
            RoomLoader.OnUnloadingAll -= PauseCycle;
            GameStatus.OnPlayerDied -= PauseCycle;
        }
        
        public void PauseCycle()
        {
            HoloplatformAnchor.OnHoloplatformLoaded += StartCycle;
            KillchainAnchor.OnKillchainLoaded += StartCycle;
            RoomLoader.OnUnloadingAll -= PauseCycle;
            GameStatus.OnPlayerDied -= PauseCycle;
            gameObject.SetActive(false);
        }
        
        public void StartCycle()
        {
            if (gameObject == null) return;
            gameObject.SetActive(true);
            RoomLoader.OnUnloadingAll += PauseCycle;
            GameStatus.OnPlayerDied += PauseCycle;
            HoloplatformAnchor.OnHoloplatformLoaded -= StartCycle;
            KillchainAnchor.OnKillchainLoaded -= StartCycle;
        }
        
        private void FixedUpdate()
        {
            Frame++;
        }
    }
}
