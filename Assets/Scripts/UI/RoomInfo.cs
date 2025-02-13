using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RoomInfo : MonoBehaviour
{
    [Header("References")]
    public Location_Base choosedlocation;
    public Image icon;
    [Header("TextMesh")]
    public TextMeshProUGUI nameLabel;
    public TextMeshProUGUI levelLabel;
    public TextMeshProUGUI goldTmp;
    public TextMeshProUGUI expTmp;
    public void SetLocation(Location_Base location)
    {
        choosedlocation = location;
        RoomContainer.choosedRoomsList = choosedlocation.choosedRoomsList;
        RoomContainer.level = choosedlocation.level;
        RefreshUI();
    }

    public void RefreshUI()
    {
        switch (LanguageChanger.Instance.languageType)
        {
            case TranslationType.russian:
                levelLabel.text = $"УРОВЕНЬ {choosedlocation.level}";
            break;
            case TranslationType.english:
                levelLabel.text = $"LEVEL {choosedlocation.level}";
                break;
        }
        for (int i = 0; i < choosedlocation.choosedRoomsList.label.Count; i++)
        {
            if (choosedlocation.choosedRoomsList.label[i].type == LanguageChanger.Instance.languageType)
            {
                nameLabel.text = $"{choosedlocation.choosedRoomsList.label[i].textRef}";
            }
        }
        goldTmp.text = $"{choosedlocation.choosedRoomsList.goldReward}";
        expTmp.text = $"{choosedlocation.choosedRoomsList.expReward}";
        icon.sprite = choosedlocation.choosedRoomsList.icon;
    }
    public void StartLocation()
    {
        Scene_Loader.Instance.ChangeScene(choosedlocation.choosedRoomsList.sceneName);
    }
}
