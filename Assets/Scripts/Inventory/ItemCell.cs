using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
public class ItemCell : MonoBehaviour, IDropHandler
{
    [Header("References")]
    [SerializeField] private protected Image icon;
    [SerializeField] private GameObject equipedIcon;
    [SerializeField] private GameObject levelBorder;
    [SerializeField] private TextMeshProUGUI levelText;
    [Header("Type")]
    public Sprite baseSprite;
    public ItemType cellType;
    [Space]
    [Header("Preferences")]
    public Item item;
    public int count;
    public int level;
    public bool equiped;
    [Header("Sounds")]
    public Sound_Events soundEvents;
    [SerializeField] private AudioClip swapSound;
    [SerializeField] private AudioClip dragSound;
    [SerializeField] private AudioClip equipSound;
    public virtual void Start()
    {
        RefreshUI();
    }
    public virtual void RefreshUI()
    {
        if(!icon) return;
        if (item)
        {
            icon.sprite = item.icon;
            icon.color = Color.white;
            levelBorder.gameObject.SetActive(true);
            levelText.text = $"{level}";
            if (count <= 0)
            {
                ClearCell();
            }
        }
        else
        {
            levelBorder.gameObject.SetActive(false);
            if (baseSprite)
            {
                icon.sprite = baseSprite;
            }
            else
            {
                icon.color = Color.clear;
            }
            if(equiped)
            {
                Unequip();
            }
        }
        equipedIcon.gameObject.SetActive(equiped);
    }
    public void Unequip()
    {
        equiped = false;
        RefreshUI();
    }
    public void ClearCell()
    {
        item = null;
        count = 0;
        equiped = false;
        RefreshUI();
    }

    public virtual void SetCell(Item setItem, int setCount, int setLevel)
    {
        item = setItem;
        count = setCount;
        level = setLevel;
        RefreshUI();
    }
    public virtual void OnDrop(PointerEventData eventData)
    {
        GameObject hoveredObj = eventData.pointerDrag;
        if (hoveredObj.TryGetComponent<ItemDragHandler>(out var itemDrag))
        {
            if (itemDrag.itemCell.item&& itemDrag.itemCell.item == item)
            {
                for (int i = 0; i < itemDrag.itemCell.item.maxCount; i++)
                {
                    if (itemDrag.itemCell.count > 0 && count < item.maxCount)
                    {
                        count += 1;
                        itemDrag.itemCell.count -= 1;
                    }
                    else
                    {
                        break;
                    }
                }
                itemDrag.itemCell.RefreshUI();
                RefreshUI();
            }
            else
            {
                if (itemDrag.itemCell.item)
                {
                    if (cellType == itemDrag.itemCell.item.itemType)
                    {
                        if (equiped)
                        {
                            Equipment.Instance.Unequip(this);
                        }
                        SwapItems(itemDrag);
                        Equipment.Instance.Equip(this);
                        itemDrag.itemCell.RefreshUI();
                        if (soundEvents)
                        {
                            soundEvents.PlayShot(equipSound);
                        }
                    }
                    else if (cellType == 0 && itemDrag.itemCell.cellType == 0)
                    {
                        SwapItems(itemDrag);
                    }
                    else if (!item && cellType == 0 && itemDrag.itemCell.cellType != 0)
                    {
                        Equipment.Instance.Unequip(itemDrag.itemCell);
                        SwapItems(itemDrag);
                    }
                }
            }
            if (soundEvents)
            {
                soundEvents.PlayShot(dragSound);
            }
        }
    }
    public void SwapItems(ItemDragHandler itemDrag)
    {
        TransferItem transferItem = new TransferItem();
        transferItem.SetItem(item, count, level);
        SetCell(itemDrag.itemCell.item, itemDrag.itemCell.count, itemDrag.itemCell.level);
        itemDrag.itemCell.SetCell(transferItem.item, transferItem.count, transferItem.level);
        itemDrag.itemCell.RefreshUI();
        if (soundEvents)
        {
            soundEvents.PlayShot(swapSound);
        }
    }
}
public class TransferItem
{
    public Item item;
    public int level;
    public int count;
    public void SetItem(Item transferItem, int transferCount, int setLevel)
    {
        item = transferItem;
        count = transferCount;
        level = setLevel;
    }
}
