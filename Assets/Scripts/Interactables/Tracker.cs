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
            public string room;
            public Vector2Int pos;
            public bool isDirty;
        }

        private List<TrackedObject> tracked = new List<TrackedObject>();
        
        public void AddToList(GameObject obj, Vector2Int pos)
        {
            if (FindIndexOfObject(obj, pos) == -1)
            {
                var newTracked = NewTrackedObject(obj, pos);             
                tracked.Add(newTracked);
            }
        }
        
        public bool IsObjectDirty(GameObject obj, Vector2Int pos)
        {
            var idx = FindIndexOfObject(obj, pos);
            if (idx >= 0) return tracked[idx].isDirty;
            return false;
        }
        
        private int FindIndexOfObject(GameObject obj, Vector2Int pos)
        {
            for (int i = 0; i < tracked.Count; i++)
            {
                var rounded = Vector2Int.RoundToInt(pos);
                var sameName = obj.name == tracked[i].name;
                var samePos = rounded == tracked[i].pos;
                var sameRoom = FindCurrentRoom() == tracked[i].room;
                if (sameName && samePos)
                    return i;
            }
            return -1;
        }
        
        public void SetObjectDirty(GameObject obj, Vector2Int pos)
        {
            if (IsObjectDirty(obj, pos)) return;
            var idx = FindIndexOfObject(obj, pos);
            var newObj = NewTrackedObject(obj, pos);
            newObj.isDirty = true;
            if (idx >= 0) tracked.RemoveAt(idx);
            tracked.Add(newObj);
        }
        
        private TrackedObject NewTrackedObject(GameObject obj, Vector2Int pos)
        {
            TrackedObject newObj;
            newObj.name = obj.name;
            newObj.pos = pos;
            newObj.isDirty = false;
            newObj.room = FindCurrentRoom();
            return newObj;
        }
        
        private string FindCurrentRoom()
        {
            return GameObject.FindObjectOfType<RoomData>().gameObject.name;
        }
    }
}
