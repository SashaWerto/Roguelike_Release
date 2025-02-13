using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Skill_Cell : MonoBehaviour
{
    [Header("References")]
    public TextMeshProUGUI levelLabel;
    [Header("Level")]
    public List<StatState> listOfStates = new List<StatState>();
    public int currentLevel;

    private void Start()
    {
        Refresh();
    }

    public void Refresh()
    {
        if (currentLevel < listOfStates.Count)
        {
            levelLabel.text = $"{currentLevel}/{listOfStates.Count}";
        }
        else
        {
            levelLabel.text = "Макс.";
        }
        Stats.Instance.Refresh();
    }
    public void ChangeStat(StatState statState)
    {
        StatType statType = statState.statTypeRef;
        switch (statType)
        {
            case StatType.health:
                Stats.Instance.StatAddHealth(statState.value);
                break;
            case StatType.damage:
                Stats.Instance.StatAddForce(statState.value);
                break;
            case StatType.defence:
                Stats.Instance.StatAddDefence(statState.value);
                break;
        }
        Refresh();
        Data_Manipulator.Instance.Save();
    }
    public void AddAttribute()
    {
        if(currentLevel >= listOfStates.Count || Stats.Instance.skillPoints <= 0) return;
        ChangeStat(listOfStates[currentLevel]);
        currentLevel++;
        Stats.Instance.skillPoints--;
        Refresh();
    }
}

[Serializable]
public class StatState
{
    public float value;
    public StatType statTypeRef;
}
