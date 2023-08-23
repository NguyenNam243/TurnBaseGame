using System.Collections;
using System.Collections.Generic;
using DG.DemiLib.Attributes;
using UnityEngine;

[DeScriptExecutionOrder(-100)]
public class PopupManager : MonoBehaviour
{
    
    public static PopupManager Instance = null;

    private List<Popup> popupList = new List<Popup>();

    public RectTransform container { get; set; }


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(Instance);
    }

    public Popup GetPopup(string popupName)
    {
        Popup popupOnPool = popupList.Find(p => p.PopupName.Equals(popupName));

        if (popupOnPool != null)
        {
            Popup _popup = popupOnPool;
            popupList.Remove(popupOnPool);
            return _popup;
        }    

        GameObject popupAsset = Resources.Load<GameObject>($"Popup/{popupName}");
        GameObject popupPrefab = Instantiate(popupAsset, container);
        Popup popup = popupPrefab.AddComponent<Popup>();
        return popup;
    }

    public void PushPopup(Popup popup)
    {
        popup.Hide();
        popupList.Add(popup);
    }
}
