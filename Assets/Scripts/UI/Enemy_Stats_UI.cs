using System;
using UnityEngine;
using TMPro;
public class Enemy_Stats_UI : MonoBehaviour
{
    public Enemy_Stats enemyStats;
    public TextMeshPro levelLabel;

    private void OnEnable()
    {
        enemyStats.OnRefresh += Refresh;
    }

    private void OnDisable()
    {
        enemyStats.OnRefresh -= Refresh;
    }

    public void Refresh()
    {
        levelLabel.text = $"{enemyStats.level}";
    }
}
