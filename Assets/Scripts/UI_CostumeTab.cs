using System;
using UnityEngine;
using UnityEngine.UI;

public class UI_CostumeTab : MonoBehaviour
{
    public CostumeType type;
    public GameObject iconNormal;
    public GameObject iconFocus;
    public GameObject tabFocus;

    public Action<CostumeType> OnSelected = null;
    
    private Toggle toggle = null;



    private void Awake()
    {
        toggle = GetComponent<Toggle>();    
        toggle.onValueChanged.AddListener(OnValueChanged);
    }

    private void OnValueChanged(bool isOn)
    {
        if (iconNormal != null)
            iconNormal.SetActive(!isOn);

        if (iconFocus != null)
            iconFocus.SetActive(isOn);

        if (tabFocus != null)
            tabFocus.SetActive(isOn);

        if (isOn)
            OnSelected?.Invoke(type);
    }

    public void ToggleOn()=> toggle.isOn = true;

    public void ToggleOff() => toggle.isOn = false;
}
