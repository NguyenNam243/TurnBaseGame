using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Item : MonoBehaviour
{
    public Image icon = null;
    public CostumeData Data { get; private set; }
    public Action<CostumeData> OnSelected = null;


    private Button button = null;


    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClicked);
    }

    private void OnClicked()
    {
        OnSelected?.Invoke(Data);
    }

    public void UpdateContent(CostumeData data)
    {
        this.Data = data;
        icon.sprite = Resources.Load<Sprite>(string.Format(GameConstants.ITEMSPATH, data.type.ToString(), data.costumeName));
    }
}
