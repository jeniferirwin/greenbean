using UnityEngine;

namespace Com.Technitaur.GreenBean.Interactables
{
    public class BouncingSkull : Mob
    {
        [SerializeField] private GameObject _collContainer = null;
        private int[] steps = { 3, 2, 2, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 0, 0, 0 };
        private int idx = 0;
        private bool dropping = false;
        private bool active = true;
        
        public override void StopMoving() => active = false;
        public override void FixedUpdate()
        {
            if (!active) return;
            base.FixedUpdate();
            if (idx >= steps.Length)
            {
                dropping = true;
                idx--;
            }
            else if (idx == -1)
            {
                dropping = false;
                idx = 0;
            }
            
            if (dropping)
            {
                _spriteContainer.transform.localPosition -= new Vector3(0, steps[idx]);
                _collContainer.transform.localPosition -= new Vector3(0, steps[idx]);
                idx--;
            }
            else
            {
                try
                {
                _spriteContainer.transform.localPosition += new Vector3(0, steps[idx]);
                _collContainer.transform.localPosition += new Vector3(0, steps[idx]);
                }
                catch (System.IndexOutOfRangeException)
                {
                    Debug.Log($"Index: {idx} | Array Length: {steps.Length}");
                    idx = 0;
                }
                idx++;
            }
        }
    }
}
