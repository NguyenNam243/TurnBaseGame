using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupEquipmentDetails : MonoBehaviour
{
    public Button closeBtn;
    public Button equipBtn;
    public Button unEquipBtn;
    public Image icon;
    public TMP_Text itemName;

    public Action<CostumeData> OnEquip = null;
    public Action<CostumeData> OnUnEquip = null;

    private Popup popup = null;

    private CostumeData Data = null;
    private UI_Attribute[] attrs;

    private void Awake()
    {
        popup = GetComponent<Popup>();
        attrs = GetComponentsInChildren<UI_Attribute>();
        closeBtn.onClick.AddListener(() => { popup.Hide(); });
        equipBtn.onClick.AddListener(OnEquipClicked);
        unEquipBtn.onClick.AddListener(OnUnEquipclicked);
    }

    public void UpdateContent(HeroData currentHero, CostumeData data)
    {
        this.Data = data;

        foreach (var attr in attrs)
            attr.UpdateContent(currentHero);

        icon.sprite = Resources.Load<Sprite>(string.Format(GameConstants.ITEMSPATH, data.type.ToString(), data.costumeName));
        itemName.text = data.costumeName;
    }

    private void OnEquipClicked()
    {
        OnEquip?.Invoke(Data);
        PopupManager.Instance.PushPopup(popup);
    }

    private void OnUnEquipclicked()
    {
        OnUnEquip?.Invoke(Data);
        PopupManager.Instance.PushPopup(popup);
    }
}
