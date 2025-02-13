using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAdder : MonoBehaviour
{
    public Item item;
    public int level;
    public int count;

    public void AddItem()
    {
        TransferItem itemTransfer = new TransferItem();
        itemTransfer.item = item;
        itemTransfer.count = count;
        itemTransfer.level = level;
        Inventory.Instance.AddItem(itemTransfer);
    }

    public void AddExp(float value)
    {
        Stats.Instance.AddExp(value);
    }
}
