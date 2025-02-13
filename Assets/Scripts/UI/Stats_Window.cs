using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Stats_Window : MonoBehaviour
{
    [Header("References")]
    public Stats stats;
    [Header("Player info")]
    public TextMeshProUGUI infoPlayer;
    public TextMeshProUGUI levelLabel;
    public TextMeshProUGUI skillsLabel;

    private void Start()
    {
        RefreshUI();
        LanguageChanger.Instance.OnLanguageChange += RefreshUI;
    }

    private void OnEnable()
    {
        stats.OnRefresh += RefreshUI;
        if(LanguageChanger.Instance)
        {
            LanguageChanger.Instance.OnLanguageChange += RefreshUI;
        }
    }
    private void OnDisable()
    {
        stats.OnRefresh -= RefreshUI;
        LanguageChanger.Instance.OnLanguageChange -= RefreshUI;
    }
    public void RefreshUI()
    {
        switch (LanguageChanger.Instance.languageType)
        {
            case TranslationType.russian:
                infoPlayer.text = $"Здоровье: {stats.health.ToString("0.0")}+({stats.equipHealth.ToString("0.0")})\n" +
                                  $"Броня: {stats.armor.ToString("0.0")}+({stats.equipArmor.ToString("0.0")})\n" +
                                  $"Сила: {stats.force.ToString("0.0")}+({stats.equipForce.ToString("0.0")})\n";
                levelLabel.text = $"Уровень: {stats.level}";
                skillsLabel.text = $"Очки навыков: {stats.skillPoints}";
                break;
            case TranslationType.english:
                infoPlayer.text = $"Health: {stats.health.ToString("0.0")}+({stats.equipHealth.ToString("0.0")})\n" +
                                  $"Defence: {stats.armor.ToString("0.0")}+({stats.equipArmor.ToString("0.0")})\n" +
                                  $"Power: {stats.force.ToString("0.0")}+({stats.equipForce.ToString("0.0")})\n";
                levelLabel.text = $"Level: {stats.level}";
                skillsLabel.text = $"Skill points: {stats.skillPoints}";
                break;
        }
    }
}
