using UnityEngine;

namespace Com.Technitaur.GreenBean.Interactables
{
    public static class HoloplatformGenerator
    {
        public static GameObject Generate(GameObject parent, int length, LayerMask layer)
        {
            GenerateCollider(parent, length, layer);
            GenerateSpriteSlots(parent, length);
            return parent;
        }
        
        private static void GenerateCollider(GameObject platform, int length, LayerMask layer)
        {
            var colliderObj = new GameObject("Collider");
            colliderObj.transform.parent = platform.transform;
            colliderObj.transform.localPosition = Vector3.zero;
            colliderObj.layer = 14;
            var newCollider = colliderObj.AddComponent<BoxCollider2D>();
            newCollider.isTrigger = true;
            var newOffset = Vector2.zero;
            newCollider.size = new Vector2(length * 8, 1);
            var newOffsetX = newCollider.size.x / 2;
            var newOffsetY = -newCollider.size.y / 2;
            newCollider.offset = new Vector2(newOffsetX, newOffsetY);
        }
        
        private static void GenerateSpriteSlots(GameObject platform, int length)
        {
            var slotContainer = new GameObject("Sprite Slots");
            slotContainer.transform.parent = platform.transform;
            slotContainer.transform.localPosition = new Vector2(4f,-4f);
            for (int i = 0; i < length; i++)
            {
                var slot = new GameObject($"Slot{i}");
                slot.transform.parent = slotContainer.transform;
                slot.transform.localPosition = new Vector2(i * 8, 0f);
                var rend = slot.AddComponent<SpriteRenderer>();
                rend.sortingOrder = 10;
                rend.sprite = null;
            }
        }
    }
}
