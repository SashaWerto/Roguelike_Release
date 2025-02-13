using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private Combat combat;
    [Header("Points")]
    [SerializeField] private Transform weaponHolder;
    [SerializeField] private Transform shieldHolder;
    [SerializeField] private Transform helmetHolder;
    [SerializeField] private Transform armorHolder;
    [Header("Base Equipment")]
    public Item startWeapon;
    private GameObject createdWeapon;
    private GameObject createdShield;
    private GameObject createdHelmet;
    private GameObject createdArmor;
    private static Equipment equipment;
    public static Equipment Instance => equipment;

    private void Awake()
    {
        equipment = this;
    }

    private void Start()
    {
        if (!createdWeapon)
        {
            ItemToSave itemRef = new ItemToSave();
            itemRef.item = startWeapon;
            itemRef.level = 1;
            itemRef.count = 1;
            Equip(itemRef);
        }
    }

    public void Unequip(ItemCell cell)
    {
        switch (cell.cellType)
        {
            case ItemType.weapon:
                if (createdWeapon)
                {
                    Destroy(createdWeapon);
                    if (combat)
                    {
                        combat.weaponAnimator = null;
                    }
                }
                break;
            case ItemType.shield:
                if (createdShield)
                {
                    Destroy(createdShield);
                }

                break;
            case ItemType.helmet:
                if (createdHelmet)
                {
                    Destroy(createdHelmet);
                }
                break;
            case ItemType.armor:
                if (createdArmor)
                {
                    Destroy(createdArmor);
                }
                break;
        }

        for (int i = 0; i < cell.item.itemStatsList.Count; i++)
        {
            switch (cell.item.itemStatsList[i].statType)
            {
                case StatType.damage:
                    Stats.Instance.equipForce -= cell.item.itemStatsList[i].valueCurve.Evaluate(cell.level);
                    break;
                case StatType.health:
                    Stats.Instance.equipHealth -= cell.item.itemStatsList[i].valueCurve.Evaluate(cell.level);
                    break;
                case StatType.defence:
                    Stats.Instance.equipArmor -= cell.item.itemStatsList[i].valueCurve.Evaluate(cell.level);
                    break;
            }
        }
        Stats.Instance.Refresh();
    }

    public void Equip(ItemCell cell)
    {
        if (cell.item.prefab)
        {
            switch (cell.cellType)
            {
                case ItemType.weapon:
                    createdWeapon = Instantiate(cell.item.prefab, weaponHolder);
                    if (combat)
                    {
                        createdWeapon.TryGetComponent<Animator>(out var animator);
                        combat.weaponAnimator = animator;
                    }
                    break;
                case ItemType.shield:
                    createdShield = Instantiate(cell.item.prefab, shieldHolder);
                    break;
                case ItemType.helmet:
                    createdHelmet = Instantiate(cell.item.prefab, helmetHolder);
                    break;
                case ItemType.armor:
                    createdArmor = Instantiate(cell.item.prefab, armorHolder);
                    break;
            }

            cell.equiped = true;
            cell.RefreshUI();
        }
        for (int i = 0; i < cell.item.itemStatsList.Count; i++)
        {
            switch (cell.item.itemStatsList[i].statType)
            {
                case StatType.damage:
                    Stats.Instance.equipForce += cell.item.itemStatsList[i].valueCurve.Evaluate(cell.level);
                    break;
                case StatType.health:
                    Stats.Instance.equipHealth += cell.item.itemStatsList[i].valueCurve.Evaluate(cell.level);
                    break;
                case StatType.defence:
                    Stats.Instance.equipArmor += cell.item.itemStatsList[i].valueCurve.Evaluate(cell.level);
                    break;
            }
        }
        Stats.Instance.Refresh();
    }

    public void Equip(ItemToSave cell)
    {
        if (cell.item.prefab)
        {
            switch (cell.item.itemType)
            {
                case ItemType.weapon:
                    createdWeapon = Instantiate(cell.item.prefab, weaponHolder);
                    if (combat)
                    {
                        createdWeapon.TryGetComponent<Animator>(out var animator);
                        combat.weaponAnimator = animator;
                    }
                    break;
                case ItemType.shield:
                    createdShield = Instantiate(cell.item.prefab, shieldHolder);
                    break;
                case ItemType.helmet:
                    createdHelmet = Instantiate(cell.item.prefab, helmetHolder);
                    break;
                case ItemType.armor:
                    createdArmor = Instantiate(cell.item.prefab, armorHolder);
                    break;
            }
        }
        for (int i = 0; i < cell.item.itemStatsList.Count; i++)
        {
            switch (cell.item.itemStatsList[i].statType)
            {
                case StatType.damage:
                    Stats.Instance.equipForce += cell.item.itemStatsList[i].valueCurve.Evaluate(cell.level);
                    break;
                case StatType.health:
                    Stats.Instance.equipHealth += cell.item.itemStatsList[i].valueCurve.Evaluate(cell.level);
                    break;
                case StatType.defence:
                    Stats.Instance.equipArmor += cell.item.itemStatsList[i].valueCurve.Evaluate(cell.level);
                    break;
            }
        }
        Stats.Instance.Refresh();
    }
}
