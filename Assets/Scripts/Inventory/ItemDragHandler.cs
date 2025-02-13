using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ItemDragHandler : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    [Header("References")]
    public ItemCell itemCell;
    [SerializeField] private GameObject dropIcon;
    private Canvas canvas;
    private Image image;
    private void Start()
    {
        image = GetComponent<Image>();
        dropIcon.SetActive(false);
    }

    private void OnDisable()
    {
        EndDrag();
    }

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        StartDrag();
    }
    public virtual void OnDrag(PointerEventData eventData)
    {
        if(!itemCell.item) return;
        transform.position = Input.mousePosition;
        if (eventData.hovered.Count <= 0)
            dropIcon.SetActive(true);
        else dropIcon.SetActive(false);       
    }
    public virtual void OnEndDrag(PointerEventData eventData)
    {
        EndDrag();
        /*if (eventData.hovered.Count <= 0 && itemCell.item)
        {
           Inventory.Instance.Drop(itemCell);
            return;
        }*/
    }

    private void StartDrag()
    {
        canvas = gameObject.AddComponent<Canvas>();
        canvas.overrideSorting = true;
    }
    private void EndDrag()
    {
        Destroy(canvas);
        dropIcon.SetActive(false);
        transform.localPosition = Vector3.zero;
    }
}
