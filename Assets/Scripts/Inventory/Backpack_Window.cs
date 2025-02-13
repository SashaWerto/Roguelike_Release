using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backpack_Window : MonoBehaviour
{
    [Header("UI")]
    public Transform main;
    public Transform backpackWindow;
    [Header("References")]
    public GameObject cellPrefab;
    public Transform content;
    private List<GameObject> createdCells = new List<GameObject>();
    private void Start()
    {
        InitiateBackpack();
    }

    public void InitiateBackpack()
    {
        if (Backpack.items.Count > 0)
        {
            main.gameObject.SetActive(false);
            backpackWindow.gameObject.SetActive(true);
            for (int i = 0; i < Backpack.items.Count; i++)
            {
                var createdCell = Instantiate(cellPrefab, content);
                createdCell.TryGetComponent<ItemCell>(out var cell);
                cell.item = Backpack.items[i].item;
                cell.level = Backpack.items[i].level;
                cell.count = Backpack.items[i].count;
                createdCells.Add(createdCell);
            }
        }
        else
        {
            main.gameObject.SetActive(true);
            backpackWindow.gameObject.SetActive(false);
        }
    }

    public void TakeAll()
    {
        foreach (var cellObj in createdCells)
        {
            if (!Inventory.Instance.CheckIsFull())
            {
                cellObj.TryGetComponent<ItemCell>(out var cell);
                if (cell.item == null)
                {
                    break;
                }
                TransferItem itemRef = new TransferItem();
                itemRef.item = cell.item;
                itemRef.level = cell.level;
                itemRef.count = cell.count;
                Inventory.Instance.AddItem(itemRef);
                cell.ClearCell();
            }
            else break;
        }
    }
    public void ClearBackpack()
    {
        Backpack.items.Clear();
        foreach (var createdCell in createdCells)
        {
            Destroy(createdCell);
        }
    }
}
