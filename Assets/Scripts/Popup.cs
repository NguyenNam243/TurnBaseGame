using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class Popup : MonoBehaviour
{
    private string popupName = "";
    public string PopupName 
    {
        get
        {
            if (string.IsNullOrEmpty(popupName))
                popupName = gameObject.name.Replace("(Clone)", "");
            return popupName;
        }
    }

    private CanvasGroup canvasGroup = null;
    private RectTransform rect = null;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rect = GetComponent<RectTransform>();
    }

    public void Show()
    {
        rect.anchoredPosition = Vector2.zero;
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = canvasGroup.interactable = true;
    }

    public void Hide()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = canvasGroup.interactable = false;
    }
}
