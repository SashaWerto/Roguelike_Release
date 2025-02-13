using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using YG;

public class LevelReward_Window : MonoBehaviour
{
    [Header("Level reference")]
    public RoomsList roomList;
    [Header("References")]
    public RectTransform itemsIconsContent;
    [Header("TextMesh")]
    public TextMeshProUGUI goldTmp;
    public TextMeshProUGUI expTmp;
    [Header("Other")]
    [SerializeField] private List<GameObject> objToDisableOnEnable;

    [Header("Buttons/AD")]
    [SerializeField] private Button goldAdButton;
    [SerializeField] private Button expAdButton;
    private List<GameObject> createdIcons = new List<GameObject>();
    private float goldMultiplier = 1f;
    private float expMultiplier = 1f;
    private int adId;

    private void Start()
    {
        if (goldAdButton)
        {
            goldAdButton.onClick.AddListener(delegate {ShowRewardAd(0);});
        }
        if (expAdButton)
        {
            expAdButton.onClick.AddListener(delegate {ShowRewardAd(1);});
        }

    }

    private void OnEnable()
    {
        Initiate();
        if(objToDisableOnEnable.Count <= 0) return;
        foreach (var obj in objToDisableOnEnable)
        {
            obj.SetActive(false);
        }
        YandexGame.RewardVideoEvent += Rewarded;
    }

    private void OnDisable()
    {
        YandexGame.RewardVideoEvent -= Rewarded;
    }

    public void ClearFromDublicates()
    {
        if (createdIcons.Count > 0)
        {
            foreach (var icon in createdIcons)
            {
                Destroy(icon);
            } 
            createdIcons.Clear();
        }
    }
    public void Initiate()
    {
        ClearFromDublicates();
        roomList = RoomContainer.choosedRoomsList;
        for (int i = 0; i < roomList.itemsReward.Count; i++)
        {
            GameObject iconRef = Instantiate(new GameObject(), itemsIconsContent);
            Image imageRef = iconRef.AddComponent<Image>();
            imageRef.sprite = roomList.itemsReward[i].icon;
            createdIcons.Add(iconRef);
        }
        Refresh();
    }
    public void Refresh()
    {
        goldTmp.text = $"{roomList.goldReward * goldMultiplier}";
        expTmp.text = $"{roomList.expReward * expMultiplier}";
    }

    public void MultiplyRewardGold(float multiplier)
    {
        goldMultiplier = 2;
        goldAdButton.gameObject.SetActive(false);
        Refresh();
    }
    public void MultiplyRewardExp(float multiplier)
    {
        expMultiplier = 2;
        expAdButton.gameObject.SetActive(false);
        Refresh();
    }

    public void GiveReward()
    {
        Wallet.Instance.AddGold(roomList.goldReward * goldMultiplier);
        Stats.Instance.AddExp(roomList.expReward * expMultiplier);
        for (int i = 0; i < roomList.itemsReward.Count; i++)
        {
            ItemToSave itemToSave = new ItemToSave();
            itemToSave.item = roomList.itemsReward[i];
            itemToSave.level = roomList.level;
            itemToSave.count = 1;
            Backpack.AddItemToBackpack(itemToSave);
        }
        Data_Manipulator.Instance.Save();
    }

    public void ShowRewardAd(int id)
    {
        YandexGame.RewVideoShow(id);
    }
    private void Rewarded(int id)
    {
        switch (id)
        {
            case 0:
                MultiplyRewardGold(2f);
                break;
            case 1:
                MultiplyRewardExp(2f);
                break;
        }
    }
}
