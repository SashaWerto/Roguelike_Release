using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomsGenerator : MonoBehaviour
{
    [Header("References")]
    public RoomManager roomManager;
    [Header("Preferences")]
    [Header("Generation")]
    public List<GameObject> roomPrefabs = new List<GameObject>();
    [HideInInspector] public List<Room> createdRooms;
    private GameObject[,] roomsArray;
    private List<Room> rooms = new List<Room>();
    private int x;
    private int y;

    private void Start()
    {
        roomPrefabs = RoomContainer.choosedRoomsList.roomsList;
        roomsArray = new GameObject[roomPrefabs.Count * 4, roomPrefabs.Count * 4];
        x = roomPrefabs.Count * 2;
        y = roomPrefabs.Count * 2;
        //CreateStartRoom();
        GenerateRooms();
    }

    /*public void CreateStartRoom()
    {
        GameObject createdRoom = Instantiate(startRoom, Vector3.zero, quaternion.identity);
        roomsArray[x, y] = createdRoom;
        createdRoom.TryGetComponent<Room>(out var room);
        room.x = x;
        room.y = y;
        rooms.Add(room);
        createdRooms.Add(room);
        roomManager.currentRoom = room;
        GenerateRooms();
    }*/
    public void GenerateRooms()
    {
        for (int i = 0; i < roomPrefabs.Count; i++)
        {
            GameObject createdRoom = Instantiate(roomPrefabs[i], Vector3.zero, Quaternion.identity);
            createdRoom.TryGetComponent<Room>(out var room);
            if(!roomManager.currentRoom)
            {
                roomManager.currentRoom = room;
            }
            bool choosedDirection = false;
            while (!choosedDirection)
            {
                var rnd = Random.Range(0, 3);
                if (rnd == 0 && !roomsArray[x, y + 1])
                {
                    y++;
                    choosedDirection = true;
                }
                if (rnd == 1 && !roomsArray[x - 1, y])
                {
                    x--;
                    choosedDirection = true;
                }
                if (rnd == 2 && !roomsArray[x + 1, y])
                {
                    x++;
                    choosedDirection = true;
                }
                if (rnd == 3 && !roomsArray[x, y - 1])
                {
                    y--;
                    choosedDirection = true;
                }
            }
            room.x = x;
            room.y = y;
            roomsArray[x, y] = createdRoom;
            rooms.Add(room);
            createdRooms.Add(room);
        }
        CheckDoors();
        foreach (var room in createdRooms)
        {
            room.main.SetActive(false);
        }
        createdRooms[0].main.SetActive(true);
    }
    public void CheckDoors()
    {
        for (int i = 0; i < createdRooms.Count; i++)
        {
            var room = createdRooms[i];
            if (roomsArray[room.x, room.y + 1])
            {
                room.upDoor.room = roomsArray[room.x, room.y + 1].GetComponent<Room>();
            }
            if (roomsArray[room.x - 1,room.y])
            {
                room.leftDoor.room = roomsArray[room.x - 1, room.y].GetComponent<Room>();
            }
            if (roomsArray[room.x + 1, room.y])
            {
                room.rightDoor.room = roomsArray[room.x + 1, room.y].GetComponent<Room>();
            }
            if (roomsArray[room.x, room.y - 1])
            {
                room.downDoor.room = roomsArray[room.x, room.y - 1].GetComponent<Room>();
            }
        }
    }
}
