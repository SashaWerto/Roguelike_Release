using System;
using UnityEngine;
public class Room : MonoBehaviour
{
    public GameObject main;
    public Transform content;
    [HideInInspector] public int x;
    [HideInInspector] public int y;
    [Header("Doors")]
    public Door upDoor;
    public Door leftDoor;
    public Door rightDoor;
    public Door downDoor;

    public void CloseAllDoors()
    {
        upDoor.LockDoors();
        leftDoor.LockDoors();
        rightDoor.LockDoors();
        downDoor.LockDoors();
    }
    public void OpenAllDoors()
    {
        upDoor.OpenDoors();
        leftDoor.OpenDoors();
        rightDoor.OpenDoors();
        downDoor.OpenDoors();
    }
}
