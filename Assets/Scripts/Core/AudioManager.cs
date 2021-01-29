using UnityEngine;

namespace Com.Technitaur.GreenBean.Core
{
    public class AudioManager : MonoBehaviour
    {
        internal static AudioSource continuousSource = null;
        internal static AudioSource oneShotSource = null;
        public static bool isMuted = true;

        public AudioClip jumping = null;
        public AudioClip killMob = null;
        public AudioClip landing = null;
        public AudioClip mobDeath = null;
        public AudioClip newLife = null;
        public AudioClip openDoor = null;
        public AudioClip collect = null;
        public AudioClip poleSlide = null;
        public AudioClip footSteps = null;

        /*
        Static members can't be set from the Unity Inspector, so I'm
        having to do a weird roundabout way of making them accessible
        to other scripts AND to this script.
        
        Sounds assigned into the inspector then get assigned to the
        below fields in Awake(). Other scripts are able to see the enum,
        so then we can call the sound with a quick parse of the enum.

        Is this ridiculous?
        */
        
        private static AudioClip p_jumping = null;
        private static AudioClip p_killMob = null;
        private static AudioClip p_landing = null;
        private static AudioClip p_mobDeath = null;
        private static AudioClip p_newLife = null;
        private static AudioClip p_openDoor = null;
        private static AudioClip p_collect = null;
        private static AudioClip p_poleSlide = null;
        private static AudioClip p_footSteps = null;
        
        private static Sound lastSound;

        private void Awake()
        {
            isMuted = true;
            oneShotSource = GameObject.Find("OneShotAudio").GetComponent<AudioSource>();
            continuousSource = GameObject.Find("ContinuousAudio").GetComponent<AudioSource>();
            p_jumping = jumping;
            p_killMob = killMob;
            p_landing = landing;
            p_mobDeath = mobDeath;
            p_newLife = newLife;
            p_openDoor = openDoor;
            p_collect = collect;
            p_poleSlide = poleSlide;
            p_footSteps = footSteps;
        }

        public enum Sound
        {
            Jump,
            KillMob,
            Land,
            Die,
            GainLife,
            OpenDoor,
            Pickup,
            PoleSlide,
            Footsteps,
            None
        }

        private static AudioClip ParseSoundType(Sound sound)
        {
            switch (sound)
            {
                case Sound.Jump: return p_jumping;
                case Sound.KillMob: return p_killMob;
                case Sound.Land: return p_landing;
                case Sound.Die: return p_mobDeath;
                case Sound.GainLife: return p_newLife;
                case Sound.OpenDoor: return p_openDoor;
                case Sound.Pickup: return p_collect;
                case Sound.PoleSlide: return p_poleSlide;
                case Sound.Footsteps: return p_footSteps;
                default: return null;
            }
        }

        public static void EmitOnce(Sound sound)
        {
            if (isMuted) return;
            if (lastSound == sound || lastSound == Sound.PoleSlide)
            {
                oneShotSource.Stop();
            }
            oneShotSource.PlayOneShot(ParseSoundType(sound));
            lastSound = sound;
        }

        public static void EmitContinuous(Sound sound)
        {
            if (isMuted) return;
            continuousSource.clip = ParseSoundType(sound);
            continuousSource.Play();
        }
        
        public static void StopEmitting()
        {
            continuousSource.Stop();
        }
    }
}
