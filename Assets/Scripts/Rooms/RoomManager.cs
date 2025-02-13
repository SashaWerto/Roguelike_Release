using System;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] private RoomsGenerator roomsGenerator;
    public List<GameObject> enemiesObj = new List<GameObject>();
    [HideInInspector] public Room currentRoom;
    private static RoomManager roomManager;
    public static Action OnCheckEntities;
    public static Action OnRoomCleared;
    private bool enemiesDetected;
    public static RoomManager Instance => roomManager;
    private void Start()
    {
        roomManager = this;
        enemiesDetected = false;
    }
    public void HideRoom(Room room)
    {
        room.main.SetActive(false);
    }
    public void ShowRoom(Room room)
    {
        room.main.SetActive(true);
        currentRoom = room;
        CheckForEnemies();
    }

    public void TransferInRoom(GameObject obj)
    {
        obj.transform.SetParent(currentRoom.content);
    }

    public void CheckForEnemies()
    {
        OnCheckEntities?.Invoke();
        if (enemiesObj.Count > 0)
        {
            currentRoom.CloseAllDoors();
            enemiesDetected = true;
        }
        else
        {
            currentRoom.OpenAllDoors();
            if (enemiesDetected)
            {
                OnRoomCleared?.Invoke();
                enemiesDetected = false;
            }
        }
    }
}
