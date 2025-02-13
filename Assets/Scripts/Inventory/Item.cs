using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ItemNull", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    [Header("Referecnes")]
    public Sprite icon;
    public List<TranslateVariant> label;
    public GameObject prefab;
    [Header("Prefences")]
    [Range(1, 99)]
    public int level = 1;
    public AnimationCurve price;
    public List<itemStat> itemStatsList;
    [Range(1, 6)]
    public int tier = 1;
    public int maxCount = 32;
    public bool isStackable = true;
    public ItemType itemType;
}
public enum ItemType
{
    nothing = 0,
    weapon = 1,
    helmet = 2,
    armor = 3,
    shield = 4,
}
[System.Serializable]
public class itemStat
{
    public StatType statType;
    public AnimationCurve valueCurve;
}