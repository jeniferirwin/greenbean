using UnityEngine;
using System.Collections.Generic;
using Com.Technitaur.GreenBean.Core;

namespace Com.Technitaur.GreenBean.Interactables
{
    public class Tracker : MonoBehaviour
    {
        public struct TrackedObject
        {
            public string name;
            public Vector2Int pos;
            public bool isDirty;
        }

        private List<TrackedObject> tracked = new List<TrackedObject>();
        
        public void CheckScene()
        {
            var trackables = FindObjectsOfType<Trackable>();
            foreach (var trackable in trackables)
            {
                var gameObject = trackable.GetGameObject();
                if (IsInList(gameObject))
                {
                    if (trackable.IsDirty) trackable.SetDirty();
                    else trackable.SetClean();
                }
                else
                {
                    AddToList(trackable);
                }
            }
        }

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

        public void AddToList(Trackable trackedItem)
        {
            var trackedGameObject = trackedItem.GetGameObject();
            if (IsInList(trackedGameObject)) return;
            var pos = Vector2Int.RoundToInt(trackedGameObject.transform.position);
            TrackedObject item;
            item.name = trackedGameObject.name;
            item.pos = pos;
            item.isDirty = false;
            tracked.Add(item);
            Debug.Log($"{item.name} was added to the list of tracked items.");
            trackedItem.SetClean();
        }
    }
}
