using UnityEngine;
using System.Collections.Generic;

namespace Com.Technitaur.GreenBean.Core
{
    public class Tracker : MonoBehaviour
    {
        public struct TrackedObject
        {
            public string name;
            public Vector2Int pos;
        }

        private List<TrackedObject> tracked = new List<TrackedObject>();
        
        
        public bool IsInList(GameObject obj)
        {
            var pos = Vector2Int.RoundToInt(obj.transform.position);
            var name = obj.name;

            foreach (var item in tracked)
            {
                if (pos == item.pos && name == item.name) return true;
            }    
            return false;
        }
        
        public void AddToList(GameObject obj)
        {
            var pos = Vector2Int.RoundToInt(obj.transform.position);
            TrackedObject item;
            item.name = obj.name;
            item.pos = pos;
            tracked.Add(item);
            Debug.Log($"{item.name} was added to the list of tracked items.");
        }
    }
}
