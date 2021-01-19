using UnityEngine;
using System.Collections.Generic;
using Com.Technitaur.GreenBean.Core;

namespace Com.Technitaur.GreenBean.Interactables
{
    public class Tracker : MonoBehaviour
    {
        private struct TrackedObject
        {
            public string name;
            public Vector2Int pos;
            public bool isDirty;
        }

        private List<TrackedObject> tracked = new List<TrackedObject>();
        
        public void AddToList(GameObject obj)
        {
            if (FindIndexOfObject(obj) == -1)
            {
                var newTracked = NewTrackedObject(obj);                
                tracked.Add(newTracked);
            }
        }
        
        public bool IsObjectDirty(GameObject obj)
        {
            var idx = FindIndexOfObject(obj);
            if (idx >= 0) return tracked[idx].isDirty;
            return false;
        }
        
        private int FindIndexOfObject(GameObject obj)
        {
            for (int i = 0; i < tracked.Count; i++)
            {
                var rounded = Vector2Int.RoundToInt(obj.transform.position);
                if (obj.name == tracked[i].name && rounded == tracked[i].pos)
                    return i;
            }
            return -1;
        }
        
        public void SetObjectDirty(GameObject obj)
        {
            if (IsObjectDirty(obj)) return;
            var idx = FindIndexOfObject(obj);
            var newObj = NewTrackedObject(obj);
            newObj.isDirty = true;
            tracked.RemoveAt(idx);
            tracked.Add(newObj);
        }
        
        private TrackedObject NewTrackedObject(GameObject obj)
        {
            TrackedObject newObj;
            newObj.name = obj.name;
            newObj.pos = Vector2Int.RoundToInt(obj.transform.position);
            newObj.isDirty = false;
            return newObj;
        }
    }
}
