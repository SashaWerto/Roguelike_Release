using UnityEngine;
using YG;
public class EmeraldRewardAd : MonoBehaviour
{
    private void OnEnable()
    {
        YandexGame.RewardVideoEvent += Rewarded;
    }

    private void OnDisable()
    {
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
            case 0:
                Wallet.Instance.AddEmeralds(5f);
                break;
        }
    }
}
