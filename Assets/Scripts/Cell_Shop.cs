using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cell_Shop : ItemCell
{
    public List<ItemToSave> possibleItems;
    public TextMeshProUGUI priceMesh;
    public List<GameObject> objToDisable;
    public bool saleForEmerald;
    private float price;
    private TransferItem choosedItem;
    public override void Start()
    {
        RefreshAssortment();
    }

    public void RefreshAssortment()
    {
        foreach (var disableObj in objToDisable)
        {
            disableObj.SetActive(true);
        }
        
        ItemToSave rnd = possibleItems[Random.Range(0, possibleItems.Count)];
        if (rnd.count <= 0)
        {
            rnd.count = 1;
        }
        choosedItem = new TransferItem();
        choosedItem.item = rnd.item;
        choosedItem.count = rnd.count;
        choosedItem.level = Mathf.Clamp(Random.Range(Stats.Instance.level - 5, Stats.Instance.level + 3), 1, 100);

        if (saleForEmerald)
        {
            price = choosedItem.item.price.Evaluate(choosedItem.level);
        }
        else
        {
            price = choosedItem.item.price.Evaluate(choosedItem.level) * 2;
        }
        
        priceMesh.text = price.ToString("0.0");
        SetCell(choosedItem.item, choosedItem.count, choosedItem.level);
        Debug.Log(level);
    }

    public void BuyItem()
    {
        if (saleForEmerald)
        {
            if (Wallet.Instance.emeralds >= price && !Inventory.Instance.CheckIsFull())
            {
                Wallet.Instance.SubstractEmeralds(price);
                Inventory.Instance.AddItem(choosedItem);
                foreach (var disableObj in objToDisable)
                {
                    disableObj.SetActive(false);
                }
                ClearCell();
                soundEvents.PlayAttachedClip();
                Data_Manipulator.Instance.Save();
            }
        }
        else
        {
            if (Wallet.Instance.gold >= price && !Inventory.Instance.CheckIsFull())
            {
                Wallet.Instance.SubstractGold(price);
                Inventory.Instance.AddItem(choosedItem);
                foreach (var disableObj in objToDisable)
                {
                    disableObj.SetActive(false);
                }
                ClearCell();
                soundEvents.PlayAttachedClip();
                Data_Manipulator.Instance.Save();
            }
        }
    }
    
    public override void OnDrop(PointerEventData eventData)
    {
    }
}
