using System;
using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
{
    [Header("References")]
    public Stats stats;
    public Image expBar;

    private void Start()
    {
        RefreshUI();
    }

    private void OnEnable()
    {
        stats.OnAddExp += RefreshUI;
        stats.OnRefresh += RefreshUI;
    }
    private void OnDisable()
    {
        stats.OnAddExp -= RefreshUI;
        stats.OnRefresh -= RefreshUI;
    }
    public void RefreshUI()
    {
        expBar.fillAmount = stats.exp / stats.expCurve.Evaluate(stats.level);
    }
}
