using System.Collections.Generic;
using UnityEngine;
public class Inventory : MonoBehaviour
{
    [Header("References")]
    public List<ItemCell> cells;
    [Header("Other")]
    [SerializeField] private GameObject itemHolderPrefab;
    private static Inventory inventory;
    public static Inventory Instance => inventory;
    private void Start()
    {
        inventory = this;
        RefreshUI();
    }
    public void RefreshUI()
    {
        for (int i = 0; i < cells.Count; i++)
        {
            cells[i].RefreshUI();
        }
    }
    public void AddItem(TransferItem transferItem)
    {
        GameObject itemHolderObj = Instantiate(itemHolderPrefab, Vector3.zero, Quaternion.identity);
        itemHolderObj.TryGetComponent<ItemHolder>(out var createdHolder);
        createdHolder.item = transferItem.item;
        createdHolder.count = transferItem.count;
        createdHolder.level = transferItem.level;
        AddItem(createdHolder);
    }
    public void AddItem(ItemHolder itemHolder)
    {
        foreach (var cell in cells)
        {
            if (cell.item && cell.item == itemHolder.item && cell.count < cell.item.maxCount && itemHolder.item.isStackable)
            {
                AddItemInStack(cell, itemHolder);
                break;
            }
            else if (!cell.item)
            {               
                AddItemInCell(cell, itemHolder);
                break;
            }
        }
    }
    private void AddItemInCell(ItemCell cell, ItemHolder itemHolder)
    {
        cell.item = itemHolder.item;
        if (itemHolder.count > itemHolder.item.maxCount)
        {
            cell.count = itemHolder.item.maxCount;
            itemHolder.count -= itemHolder.item.maxCount;
            AddItem(itemHolder);
        }
        else
        {
            cell.count = itemHolder.count;
            cell.level = itemHolder.level;
        }
        cell.RefreshUI();
        Destroy(itemHolder.gameObject);
    }
    private void AddItemInStack(ItemCell cell, ItemHolder itemHolder)
    {
        int checkAllCount = itemHolder.count + cell.count;
        if (checkAllCount <= cell.item.maxCount)
        {
            cell.count += itemHolder.count;            
            Destroy(itemHolder.gameObject);
            cell.RefreshUI();
            return;
        }
        if (cell.count < cell.item.maxCount)
        {
            for (int y = 0; y < checkAllCount; y++)
            {
                cell.count++;
                itemHolder.count--;
                if (cell.count >= cell.item.maxCount)
                {
                    AddItem(itemHolder);
                    cell.RefreshUI();
                    break;
                }
                if (itemHolder.count <= 0)
                {
                    Destroy(itemHolder.gameObject);
                    break;
                }
            }
        }
        else AddItem(itemHolder);
    }

    public bool CheckIsFull()
    {
        bool isFull = true;
        for (int i = 0; i < cells.Count; i++)
        {
            if (cells[i].item == null)
            {
                isFull = false;
                break;
            }
        }
        if (isFull)
        {
            ShowMessageFull();
        }
        return isFull;
    }

    public void ShowMessageFull()
    {
        switch (LanguageChanger.Instance.languageType)
        {
            case TranslationType.russian:
                UI_Messages.Instance.ShowMessage("Нет места","Освободите место в инвентаре", 4f);
                break;
            case TranslationType.english:
                UI_Messages.Instance.ShowMessage("Not enough space","Make room in your inventory", 4f);
                break;
        }
    }
    public void Drop(ItemCell itemCell)
    {
        if(itemCell.equiped)
        {
            Equipment.Instance.Unequip(itemCell);
        }
        itemCell.ClearCell();
    }
}
