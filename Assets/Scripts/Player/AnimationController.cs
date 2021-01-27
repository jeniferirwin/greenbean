using UnityEngine;

namespace Com.Technitaur.GreenBean.Player
{
    public class AnimationController : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer rend = null;

        [SerializeField] private Sprite jumpingFrame = null;
        [SerializeField] private Sprite standingFrame = null;
        [SerializeField] private Sprite mountingLadderFrame = null;
        [SerializeField] private Sprite poleSlidingFrame = null;

        [SerializeField] private Sprite[] runningFrames = null;
        [SerializeField] private Sprite[] climbingLadderFrames = null;
        [SerializeField] private Sprite[] climbingRopeFrames = null;
        [SerializeField] private Sprite[] fallingDeathFrames = null;
        [SerializeField] private Sprite[] mobDeathFrames = null;
        [SerializeField] private GameObject smokeParticle = null;

        private int ticks = 0;
        private bool swap = false;
        private int phase = 0;

        public void MountLadder() => rend.sprite = mountingLadderFrame;
        public void Jump() => rend.sprite = jumpingFrame;
        public void Start()
        {
            ticks = 0;
            phase = 0;
            swap = false;
        }

        public void ClearSprite() => rend.sprite = null;

        public void FallDeath()
        {
            if (swap == false)
            {
                rend.sprite = fallingDeathFrames[0];
            }
            else
            {
                rend.sprite = fallingDeathFrames[1];
            }
            ticks++;
            if (ticks == 3)
            {
                swap = !swap;
                ticks = 0;
            }
        }

        public void Climb(int direction, bool isLadder)
        {
            if (direction > 1) direction = 1;
            if (direction < -1) direction = -1;

            ticks += direction;

            if (isLadder)
            {
                if (ticks <= 0) ticks = climbingLadderFrames.Length - 1;
                if (ticks >= climbingLadderFrames.Length) ticks = 0;
                rend.sprite = climbingLadderFrames[ticks];
            }
            else
            {
                if (ticks <= 0) ticks = climbingRopeFrames.Length - 1;
                if (ticks >= climbingRopeFrames.Length) ticks = 0;
                rend.sprite = climbingRopeFrames[ticks];
            }
        }

        public void ResetTicks() => ticks = 0;
        public void Idle()
        {
            if (rend.sprite != standingFrame) rend.sprite = standingFrame;
        }

        public void Walk()
        {
            if (ticks >= runningFrames.Length) ticks = 0;
            rend.sprite = runningFrames[ticks];
            ticks++;
        }

        public void Orient(float xDir)
        {
            var sprite = rend.gameObject.transform;
            sprite.rotation = Quaternion.identity;
            if (xDir < 0)
            {
                sprite.Rotate(new Vector3(0, 180, 0));
            }
        }
        public void Slide()
        {
            rend.sprite = poleSlidingFrame;
        }

        public void MobDeath()
        {
            if (swap) Orient(-1);
            else Orient(1);
            rend.sprite = mobDeathFrames[phase];
            ticks++;
            if (ticks == 1)
            {
                ticks = 0;
                phase++;
                if (phase == 2)
                {
                    phase = 0;
                    swap = !swap;
                }
            }
        }
        
        public void FireDeath()
        {
            rend.sprite = null;
            GameObject.Instantiate(smokeParticle, transform.position - new Vector3(0, 5), Quaternion.identity);
        }
    }
}
