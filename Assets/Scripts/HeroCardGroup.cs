using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroCardGroup : IToggleGroup<UI_HeroCard, HeroData>
{


    protected void Awake()
    {
        Initialized(ConfigDataHelper.UserData.heroes);
    }

    protected override void OnCardSelected(HeroData Data)
    {
        
    }
}
