using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class SellBar : MonoBehaviour, IDropHandler
{
    public Animator animator;
    public Sound_Events soundEvents;
    
    public void OnDrop(PointerEventData eventData)
    {
        GameObject hoveredObj = eventData.pointerDrag;
        if (hoveredObj.TryGetComponent<ItemDragHandler>(out var itemDrag))
        {
            if(!itemDrag.itemCell.item) return;
            Wallet.Instance.AddGold(itemDrag.itemCell.item.price.Evaluate(itemDrag.itemCell.level));
            itemDrag.itemCell.ClearCell();
            animator.SetTrigger("Sell");
            if (soundEvents)
            {
                soundEvents.PlayAttachedClip();
            }
        }
    }
}
