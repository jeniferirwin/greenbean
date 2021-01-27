using UnityEngine;

namespace Com.Technitaur.GreenBean.Player
{
    public class AnimationController : MonoBehaviour
    {
        [SerializeField] private Controller player;
        [SerializeField] private SpriteRenderer rend;

        [SerializeField] private Sprite jumpingFrame;
        [SerializeField] private Sprite standingFrame;
        [SerializeField] private Sprite mountingLadderFrame;
        [SerializeField] private Sprite poleSlidingFrame;

        [SerializeField] private Sprite[] runningFrames;
        [SerializeField] private Sprite[] climbingLadderFrames;
        [SerializeField] private Sprite[] climbingRopeFrames;
        [SerializeField] private Sprite[] fallingDeathFrames;
        [SerializeField] private Sprite[] mobDeathFrames;
        [SerializeField] private Sprite[] fireDeathFrames;

        private int ticks;
        private bool swap;
        private int phase;

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
    }
}
