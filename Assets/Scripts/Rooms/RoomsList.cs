using UnityEngine;
using System.Collections.Generic;
[CreateAssetMenu(fileName = "RoomListNull", menuName = "Rooms/RoomsList")]
public class RoomsList : ScriptableObject
{
    [Header("References")]
    public List<GameObject> roomsList;
    [Header("GFX")]
    public List<TranslateVariant> label;
    public int level = 1;
    public string sceneName;
    public Sprite icon;
    [Header("Reward/Item's Content")]
    public List<Item> itemsReward;
    public float goldReward = 100f;
    public float expReward = 200f;
}
