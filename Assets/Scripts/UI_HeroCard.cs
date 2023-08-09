using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_HeroCard : IToggle<HeroData>
{
    [Header("Object Reference")]
    [SerializeField] private Image background = null;
    [SerializeField] private Image heroIcon = null;
    [SerializeField] private Image levelLable = null;
    [SerializeField] private TMP_Text levelTxt = null;

    public override void Initialized(HeroData Data)
    {
        base.Initialized(Data);
        background.sprite = Resources.Load<Sprite>(string.Format(GameConstants.CARDHERO, "Background", Data.rarity.ToString()));
        heroIcon.sprite = Resources.Load<Sprite>(string.Format(GameConstants.CARDHERO, "Icon", Data.name));
        levelLable.sprite = Resources.Load<Sprite>(string.Format(GameConstants.CARDHERO, "Lable", Data.rarity.ToString()));
        levelTxt.text = Data.level.ToString();
    }
}
