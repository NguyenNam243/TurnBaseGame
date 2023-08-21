using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Item : MonoBehaviour
{
    public Image icon = null;
    
    
    public void UpdateContent(CostumeData data)
    {
        icon.sprite = Resources.Load<Sprite>(string.Format(GameConstants.ITEMSPATH, data.type.ToString(), data.costumeName));
    }
}
