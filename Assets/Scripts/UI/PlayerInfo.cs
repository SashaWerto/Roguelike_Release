using UnityEngine;
using TMPro;
using YG;

public class PlayerInfo : MonoBehaviour
{
    [Header("UI/References")] 
    [SerializeField] private TextMeshProUGUI playerName;

    private void Start()
    {
        Refresh();
    }

    public void Refresh()
    {
        playerName.text = YandexGame.playerName;
    }
}
