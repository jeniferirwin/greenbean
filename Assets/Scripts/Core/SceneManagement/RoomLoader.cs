using System;
using UnityEngine;

namespace Com.Technitaur.GreenBean.Core
{
    public static class RoomLoader
    {
        public enum Direction
        {
            Up,
            Down,
            Left,
            Right,
            None
        }

        public static Vector2Int lastSpawnPos;
        public delegate void UnloadingAll();
        public static event UnloadingAll OnUnloadingAll;

        public delegate void FoundCyclicObjects();
        public static event FoundCyclicObjects OnFoundCyclicObjects;

        public static void Load(Direction direction, GameObject player)
        {
            player.SetActive(false);
            UnloadAll();
            FindRoomPrefab(direction);
            SetPlayerPosition(player, direction);
            player.SetActive(true);
        }
        
        public static GameObject FindRoomPrefab(Direction direction)
        {
            var currentRoom = GameObject.FindObjectOfType<RoomData>();
            string roomName = currentRoom.name;
            char colorGrade = roomName[0];
            string alphabet = "YZABCDEFGHIJ";
            int idx = alphabet.IndexOf(colorGrade);
            int roomNumber = 0;
            if (roomName[1].ToString() == "1") roomNumber += 10;
            if (Int32.TryParse(roomName[2].ToString(), out int num)) roomNumber += num;
            char upDirection = alphabet[idx - 1];
            char downDirection = alphabet[idx + 1];
            char neutYDirection = alphabet[idx];
            int leftDirection = roomNumber - 1;
            int rightDirection = roomNumber + 1;
            int neutXDirection = roomNumber;
            string newName = "";
            switch (direction)
            {
                case Direction.Up:
                    newName = upDirection + roomNumber.ToString().PadLeft(2,'0');
                    break;
                case Direction.Down:
                    newName = downDirection + roomNumber.ToString().PadLeft(2,'0');
                    break;
                case Direction.Left:
                    newName = neutYDirection + leftDirection.ToString().PadLeft(2,'0');
                    break;
                case Direction.Right:
                    newName = neutYDirection + rightDirection.ToString().PadLeft(2,'0');
                    break;
                default:
                    newName = neutYDirection + neutXDirection.ToString().PadLeft(2,'0');
                    break;
            }
            var pos = Vector3.zero;
            var rot = Quaternion.identity;
            return GameObject.Instantiate(Resources.Load<GameObject>($"Rooms/{newName}"), pos, rot);
        }

        public static void ReloadCurrentRoom(GameObject player) => Load(Direction.None, player);
        public static void Reload(Room room, GameObject player)
        {
            player.SetActive(false);
            UnloadAll();
            InstantiateRoomPrefab(room);
            player.transform.position = (Vector2)lastSpawnPos;
            player.SetActive(true);
        }

        private static void SetPlayerPosition(GameObject player, Direction direction)
        {
            if (direction == Direction.None)
            {
                player.transform.position = (Vector2) lastSpawnPos;
                return;
            }
            var newPos = Vector2Int.RoundToInt(player.transform.position);
            switch (direction)
            {
                case Direction.Right: newPos.x = -155; break;
                case Direction.Left: newPos.x = 155; break;
                case Direction.Up: newPos.y = -95; break;
                case Direction.Down: newPos.y = 95; break;
                default: break;
            }
            lastSpawnPos = newPos;
            player.transform.position = (Vector2)newPos;
        }

        private static void InstantiateRoomPrefab(Room room)
        {
            var path = $"Rooms/{room.ToString()}";
        }

        private static void UnloadAll()
        {
            OnUnloadingAll?.Invoke();
            foreach (var data in FindAllRoomData())
            {
                GameObject.Destroy(data.gameObject);
            }
        }

        private static RoomData[] FindAllRoomData() => GameObject.FindObjectsOfType<RoomData>();
        private static RoomData FindRoomData() => GameObject.FindObjectOfType<RoomData>();
    }
}