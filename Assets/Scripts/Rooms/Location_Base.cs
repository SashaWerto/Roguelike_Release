using UnityEngine;
using TMPro;

public class Location_Base : MonoBehaviour
{
    [Header("References")]
    public RoomInfo roomInfo;
    [Header("Rooms")]
    public RoomsList choosedRoomsList;

    [Header("UI/TextMesh")]
    public TextMeshProUGUI levelInfo;
    [HideInInspector] public int level;

    private void Start()
    {
        level = choosedRoomsList.level;
        levelInfo.text = $"{level}";
    }

    public void SetLocation()
    {
        roomInfo.SetLocation(this);
    }
}
