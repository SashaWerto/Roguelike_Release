using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class Room_EntityInfo : MonoBehaviour
{
    public Transform entityWindow;
    public TextMeshProUGUI entityCountText;
    private void OnEnable()
    {
        RoomManager.OnCheckEntities += Refresh;
    }
    private void OnDisable()
    {
        RoomManager.OnCheckEntities -= Refresh;
    }
    public void Refresh()
    {
        entityCountText.text = $"{RoomManager.Instance.enemiesObj.Count}";
        if (RoomManager.Instance.enemiesObj.Count > 0)
        {
            entityWindow.gameObject.SetActive(true);
        }
        else
        {
            entityWindow.gameObject.SetActive(false);
        }
    }
}
