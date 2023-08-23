using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasRoot : MonoBehaviour
{
    public static RectTransform root;
    public static RectTransform common;
    public RectTransform commonContainer = null;
    public RectTransform canvasRoot = null;
    public RectTransform popupContainer = null;


    private void Awake()
    {
        root = canvasRoot;
        common = commonContainer;
        PopupManager.Instance.container = popupContainer;
    }
}
