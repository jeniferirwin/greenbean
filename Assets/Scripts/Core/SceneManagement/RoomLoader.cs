using UnityEngine;

namespace Com.Technitaur.GreenBean.Core
{
    public static class RoomLoader
    {
        public static void Load(Room room, Direction direction, GameObject player)
        {
            player.SetActive(false);
            UnloadAll();
            InstantiateRoomPrefab(room);
            SetPlayerPosition(player, direction);
            player.SetActive(true);
        }

        private static void SetPlayerPosition(GameObject player, Direction direction)
        {
            var newPos = player.transform.position;
            switch (direction)
            {
                case Direction.Right: newPos.x = -155; break;
                case Direction.Left: newPos.x = 155; break;
                case Direction.Up: newPos.y = -95; break;
                case Direction.Down: newPos.y = 95; break;
                default: break;
            }
            player.transform.position = newPos;
        }
        
        private static void InstantiateRoomPrefab(Room room)
        {
            var pos = Vector3.zero;
            var rot = Quaternion.identity;
            var path = $"Rooms/{room.ToString()}";
            var newRoom = GameObject.Instantiate(Resources.Load<GameObject>(path), pos, rot);
        }

        private static void UnloadAll()
        {
            foreach (var data in FindAllRoomData())
            {
                GameObject.Destroy(data.gameObject);
            }
        }

        private static RoomData[] FindAllRoomData() => GameObject.FindObjectsOfType<RoomData>();
        private static RoomData FindRoomData() => GameObject.FindObjectOfType<RoomData>();
    }
}