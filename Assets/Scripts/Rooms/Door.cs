using Unity.VisualScripting;
using UnityEngine;
public class Door : MonoBehaviour
{
    public Room room;
    public Transform enterPoint;
    public DoorType doorType;
    public GameObject main;
    public Transform openedVer;
    public Transform closedVer;
    [HideInInspector] public bool isOpened = true;

    private void OnEnable()
    {
        if (room)
        {
            main.SetActive(true);
        }
        else
        {
            main.SetActive(false);
        }
    }
    public void ChangeRoom()
    {
        if(!isOpened) return;
        RoomManager.Instance.HideRoom(RoomManager.Instance.currentRoom);
        RoomManager.Instance.ShowRoom(room);
        var playerMovement = FindObjectOfType<TopDownMovement>();
        switch (doorType)
        {
            case DoorType.Up:
                playerMovement.rigidbody.transform.position = room.downDoor.enterPoint.position;
                break;
            case DoorType.Left:
                playerMovement.rigidbody.transform.position = room.rightDoor.enterPoint.position;
                break;
            case DoorType.Right:
                playerMovement.rigidbody.transform.position = room.leftDoor.enterPoint.position;
                break;
            case DoorType.Down:
                playerMovement.rigidbody.transform.position = room.upDoor.enterPoint.position;
                break;
        }
        AstarPath.active.Scan();
    }

    public void OpenDoors()
    {
        isOpened = true;
        openedVer.gameObject.SetActive(true);
        closedVer.gameObject.SetActive(false);
    }

    public void LockDoors()
    {
        isOpened = false;
        openedVer.gameObject.SetActive(false);
        closedVer.gameObject.SetActive(true);
    }
}
