using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasRoot : MonoBehaviour
{
    public static RectTransform root;
    public RectTransform common = null;

    private void Awake()
    {
        root = common;

    }
}
