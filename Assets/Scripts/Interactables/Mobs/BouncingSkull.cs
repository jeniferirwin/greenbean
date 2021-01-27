using UnityEngine;

namespace Com.Technitaur.GreenBean.Interactables
{
    public class BouncingSkull : Mob
    {
        [SerializeField] private GameObject _collContainer;
        private int[] steps = { 3, 2, 2, 2, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1 };
        private int idx;
        private bool dropping;
        
        public override void Start()
        {
            base.Start();
            dropping = false;
            idx = 0;    
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if (idx >= steps.Length) dropping = true;
            else if (idx == 0) dropping = false;
            
            if (dropping)
            {
                idx--;
                _spriteContainer.transform.localPosition -= new Vector3(0, steps[idx]);
                _collContainer.transform.localPosition -= new Vector3(0, steps[idx]);
            }
            else
            {
                idx++;
                _spriteContainer.transform.localPosition += new Vector3(0, steps[idx]);
                _collContainer.transform.localPosition += new Vector3(0, steps[idx]);
            }
        }
    }
}
