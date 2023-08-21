using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Attribute : MonoBehaviour
{
    public AttributeType type;
    public TMP_Text valueTxt = null;


    public void UpdateContent(HeroData currentHero)
    {
        float finalValue = currentHero.attributes[type].value;

        foreach (var costume in currentHero.costumes)
        {
            if (costume.Value.attributes.ContainsKey(type))
                finalValue += costume.Value.attributes[type].value;
        }

        valueTxt.text = finalValue.ToString();
    }
}
