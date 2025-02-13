using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TooltipManager : MonoBehaviour
{
    public RectTransform tooltip;
    public TextMeshProUGUI info;
    public Image icon;
    public CanvasGroup canvasGroup;
    private float targetAlpha;
    
    private static TooltipManager tooltipManager;
    public static TooltipManager Instance => tooltipManager;

    private void Start()
    {
        HideTooltip();
    }

    private void Awake()
    {
        tooltipManager = this;
    }

    private void Update()
    {
        canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, targetAlpha, 4f * Time.deltaTime);
        if (Input.mousePosition.x > Screen.width / 2)
        {
            tooltip.pivot = new Vector2(1,1);
        }
        else
        {
            tooltip.pivot = new Vector2(0,1);
        }
        tooltip.position = Input.mousePosition;
    }

    public void ShowTooltip(string textRef, Sprite iconRef = null)
    {
        targetAlpha = 1f;
        tooltip.gameObject.SetActive(true);
        info.text = textRef;
        if (iconRef)
        {
            icon.sprite = iconRef;
        }
    }

    public void HideTooltip()
    {
        targetAlpha = 0;
        canvasGroup.alpha = 0;
        tooltip.gameObject.SetActive(false);
    }
}
