using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Cosutme : MonoBehaviour
{
    public CostumeType type;
    public Image icon;

    private Sprite iconDefault = null;
    private Color defaultColor;

    public Action OnClicked = null;
    public Action OnUpdateData = null;

    private Button button = null;
    public CostumeData Data { get; private set; }

    private void Awake()
    {
        iconDefault = icon.sprite;
        defaultColor = icon.color;

        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    public void UpDateContent(CostumeData data)
    {
        this.Data = data;

        if (data == null)
        {
            icon.sprite = iconDefault;
            icon.color = defaultColor;
            return;
        }
        
        icon.sprite = Resources.Load<Sprite>(string.Format(GameConstants.ITEMSPATH, data.type.ToString(), data.costumeName));
        icon.color = new Color(1, 1, 1, 1);

        OnUpdateData?.Invoke();
    }

    private void OnClick()
    {
        OnClicked?.Invoke();
    }
}
