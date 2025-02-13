using UnityEngine;
using TMPro;
public class Wallet_UI : MonoBehaviour
{
    [Header("References")]
    public Wallet wallet;
    [Header("UI")]
    public TextMeshProUGUI goldTextMesh;
    public TextMeshProUGUI emeraldsTextMesh;

    private void OnEnable()
    {
        wallet.OnChange += Refresh;
    }

    private void OnDisable()
    {
        wallet.OnChange -= Refresh;
    }

    public void Refresh()
    {
        goldTextMesh.text = wallet.gold.ToString("0");
        emeraldsTextMesh.text = wallet.emeralds.ToString("0");
    }
}
