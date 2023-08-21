using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public RectTransform itemsContainer = null;
    public Image heroAvatar = null;
    public GameObject itemAsset = null;


    private List<CostumeData> items = null;
    private List<HeroData> heroes = new List<HeroData>();
    private List<UI_Attribute> ui_Attributes = null;

    private HeroData currentHero = null;
    private int currentHeroIndex = 0;


    private void Awake()
    {
        items = ConfigDataHelper.ItemsData;
        ui_Attributes = GetComponentsInChildren<UI_Attribute>().ToList();

        foreach (var hero in ConfigDataHelper.UserData.heroes)
        {
            heroes.Add(hero.Value);
        }

        currentHero = heroes[currentHeroIndex];
        heroAvatar.sprite = Resources.Load<Sprite>(string.Format(GameConstants.CARDHERO, "Icon", currentHero.name));
        UpdateAttributes();

        foreach (var item in items)
        {
            GameObject obj = Instantiate(itemAsset, itemsContainer);
            UI_Item ui_Item = obj.GetComponent<UI_Item>();
            ui_Item.UpdateContent(item);
        }
    }

    public void UpdateAttributes()
    {
        foreach (var att in ui_Attributes)
        {
            att.UpdateContent(currentHero);
        }
    }

    public void NextHero()
    {
        currentHeroIndex++;
        if (currentHeroIndex >= heroes.Count)
            currentHeroIndex = 0;

        currentHero = heroes[currentHeroIndex];

        heroAvatar.sprite = Resources.Load<Sprite>(string.Format(GameConstants.CARDHERO, "Icon", currentHero.name));

        UpdateAttributes();
    }

    public void PreviourHero()
    {
        currentHeroIndex--;
        if (currentHeroIndex < 0)
            currentHeroIndex = heroes.Count - 1;

        currentHero = heroes[currentHeroIndex];

        heroAvatar.sprite = Resources.Load<Sprite>(string.Format(GameConstants.CARDHERO, "Icon", currentHero.name));

        UpdateAttributes();
    }
}
