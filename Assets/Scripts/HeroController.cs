using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroController : MonoBehaviour
{
    

    public HeroData HeroData { get; private set; }

    private Image avatar = null;
    private RectTransform rect = null;



    public void Initialized(HeroData HeroData)
    {
        this.HeroData = HeroData;
        avatar = GetComponent<Image>();
        rect = GetComponent<RectTransform>();
        avatar.sprite = Resources.Load<Sprite>(string.Format(GameConstants.CARDHERO, "Icon", HeroData.name));
        HeroSizeStore.HeroSize size = ConfigDataHelper.HeroSizeStore.sizes.Find(s => s.heroName.Equals(HeroData.name));
        if (size != null)
            rect.sizeDelta = size.sizeDelta;
    }
}
