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
            GetNextRoomPrefab(direction);
            SetPlayerPosition(player, direction);
            player.SetActive(true);
        }

        public static GameObject GetNextRoomPrefab(Direction direction)
        {
            char grade;
            int num;
            (grade, num) = GetCurrentRoomInfo();
            return CreateRoom(GetNextRoomName(grade, num, direction));
        }

        public static string GetNextRoomName(char grade, int num, Direction direction)
        {
            var newGrade = GetNextRoomGrade(grade, direction);
            var newNumber = GetNextRoomNumber(num, direction);
            return newGrade + newNumber.ToString();
        }

        public static char GetNextRoomGrade(char grade, Direction direction)
        {
            string gradeArray = "YZABCDEFGHIJK";
            int newIDX;

            switch (direction)
            {
                case Direction.Up:
                    newIDX = gradeArray.IndexOf(grade) - 1;
                    break;
                case Direction.Down:
                    newIDX = gradeArray.IndexOf(grade) + 1;
                    break;
                case Direction.Left:
                case Direction.Right:
                default:
                    newIDX = gradeArray.IndexOf(grade);
                    break;
            }
            return gradeArray[newIDX];
        }

        public static int GetNextRoomNumber(int num, Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                case Direction.Down:
                {
                    return num;
                }
                case Direction.Left:
                {
                    int newNum = num - 1;
                    if (newNum < 1) return 1;
                    break;
                }
                case Direction.Right:
                {
                    int newNum = num + 1;
                    if (newNum > 11) return 11;
                    break;
                }
            }
            return num;
        }

        public static GameObject CreateRoom(string newName)
        {
            var pos = Vector3.zero;
            var rot = Quaternion.identity;
            return GameObject.Instantiate(Resources.Load<GameObject>($"Rooms/{newName}"), pos, rot);
        }

        public static (char, int) GetCurrentRoomInfo()
        {
            var currentRoom = GameObject.FindObjectOfType<RoomData>().gameObject.name;
            var grade = currentRoom[0];
            var number = currentRoom.Substring(1);
            if (Int32.TryParse(number, out int num))
            {
                return (grade, num);
            }
            return ('A', 10);
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
                player.transform.position = (Vector2)lastSpawnPos;
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