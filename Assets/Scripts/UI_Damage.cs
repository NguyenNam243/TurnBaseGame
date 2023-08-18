using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Damage : MonoBehaviour
{
    private TMP_Text text = null;
    private Animator ator = null;


    public void UpdateDamage(float damage, Vector3 anchorPos)
    {
        ator = GetComponentInChildren<Animator>();
        text = GetComponentInChildren<TMP_Text>();
        ator.Rebind();
        text.rectTransform.anchorMin = Vector2.zero;
        text.text = damage.ToString();
        gameObject.GetComponent<RectTransform>().anchoredPosition = anchorPos;
        gameObject.SetActive(true);
    }
}
