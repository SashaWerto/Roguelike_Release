using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tooltip_Cell : Tooltip
{
    public ItemCell cell;
    public override void OnPointerEnter(PointerEventData eventData)
    {
        if(!cell.item) return;
        base.OnPointerEnter(eventData);
        string message = "";
        for (int i = 0; i < cell.item.label.Count; i++)
        {
            if(cell.item.label[i].type == LanguageChanger.Instance.languageType)
            {
                message += $"'{cell.item.label[i].textRef}'\n";
            }
        }
        switch (LanguageChanger.Instance.languageType)
        {
            case TranslationType.russian:
                message += $"Уровень {cell.level}";
                for (int i = 0; i < cell.item.itemStatsList.Count; i++)
                {
                    var stat = cell.item.itemStatsList[i];
                    switch (stat.statType)
                    {
                        case StatType.damage:
                            message += $"\nУрон: {stat.valueCurve.Evaluate(cell.level).ToString("0.0")}";
                            break;
                        case StatType.defence:
                            message += $"\nЗащита: {stat.valueCurve.Evaluate(cell.level).ToString("0.0")}";
                            break;
                        case StatType.health:
                            message += $"\nЗдоровье: {stat.valueCurve.Evaluate(cell.level).ToString("0.0")}";
                            break;
                    }
                }
                message += $"\nЦена: {cell.item.price.Evaluate(cell.level).ToString("0.0")}";
                break;
            
            case TranslationType.english: 
                message += $"Level {cell.level}";
                for (int i = 0; i < cell.item.itemStatsList.Count; i++)
                {
                    var stat = cell.item.itemStatsList[i];
                    switch (stat.statType)
                    {
                        case StatType.damage:
                            message += $"\nDamage: {stat.valueCurve.Evaluate(cell.level).ToString("0.0")}";
                            break;
                        case StatType.defence:
                            message += $"\nDefence: {stat.valueCurve.Evaluate(cell.level).ToString("0.0")}";
                            break;
                        case StatType.health:
                            message += $"\nHealth: {stat.valueCurve.Evaluate(cell.level).ToString("0.0")}";
                            break;
                    }
                }
                message += $"\nCost: {cell.item.price.Evaluate(cell.level).ToString("0.0")}";
                break;
        }
        TooltipManager.Instance.ShowTooltip(message, cell.item.icon);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        TooltipManager.Instance.HideTooltip();
    }
}
