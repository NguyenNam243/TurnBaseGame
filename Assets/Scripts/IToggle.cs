using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IToggle<T> : MonoBehaviour where T : class
{

    public bool Selected => toggle.isOn;
    public T Data { get; private set; }

    public Action<T> OnSelected = null;

    private Toggle toggle = null;
    private Animator ator = null;

    protected virtual void Awake()
    {
        ator = GetComponent<Animator>();
        toggle = GetComponent<Toggle>();
        toggle.group = GetComponentInParent<ToggleGroup>();
        toggle.onValueChanged.AddListener(OnValueChanged);
        ator.speed = 0;
    }

    public virtual void Initialized(T Data)
    {
        this.Data = Data;
    }

    protected void OnValueChanged(bool isOn)
    {
        ator.Rebind();
        ator.speed = isOn ? 1 : 0;
        if (isOn)
            OnSelected?.Invoke(Data);
    }

    public void ToggleOff() => toggle.isOn = false;

    public void Hide() => gameObject.SetActive(false);

}
