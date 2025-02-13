using System;
using UnityEngine;
using UnityEngine.UI;
using YG;
public class GameEventsCaller : MonoBehaviour
{
    [Header("UI/References")]
    [SerializeField] private GameObject rewardCanvas;
    [SerializeField] private GameObject deathCanvas;

    [Header("Player references")]
    [SerializeField] private Player_Health playerHealth;
    private static GameEventsCaller gameEventsCaller;
    public static GameEventsCaller Instance => gameEventsCaller;
    private void Awake()
    {
        gameEventsCaller = this;
    }
    private void OnEnable()
    {
        playerHealth.OnDeath += ShowOnDeathWindow;
        YandexGame.RewardVideoEvent += Rewarded;
    }

    private void OnDisable()
    {
        playerHealth.OnDeath -= ShowOnDeathWindow;
        YandexGame.RewardVideoEvent -= Rewarded;
    }
    public void ShowRewardAd(int id)
    {
        YandexGame.RewVideoShow(id);
    }
    private void Rewarded(int id)
    {
        switch (id)
        {
            case 3:
                RevivePlayer();
                break;
        }
    }

    public void RevivePlayer()
    {
        playerHealth.Revive();
        deathCanvas.SetActive(false);
    }
    public void ShowOnDeathWindow()
    {
        deathCanvas.SetActive(true);
    }

    public void ShowRewardWindow()
    {
        rewardCanvas.SetActive(true);
    }
}
