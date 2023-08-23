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

    private Dictionary<CostumeType, UI_CostumeTab> ui_Tabs = new Dictionary<CostumeType, UI_CostumeTab>();
    private List<UI_Item> ui_Items = new List<UI_Item>();

    private Dictionary<CostumeType, UI_Cosutme> ui_Costumes = new Dictionary<CostumeType, UI_Cosutme>();

    private HeroData currentHero = null;
    private int currentHeroIndex = 0;


    private void Awake()
    {
        items = ConfigDataHelper.ItemsData;
        ui_Attributes = GetComponentsInChildren<UI_Attribute>().ToList();

        UI_Cosutme[] uis = GetComponentsInChildren<UI_Cosutme>();
        foreach (var costume in uis)
        {
            ui_Costumes.Add(costume.type, costume);
            costume.OnClicked = () => { OnItemSelected(costume.Data); };
            costume.OnUpdateData = () =>
            {
                UserData newData = (UserData)ConfigDataHelper.UserData.Clone();
                newData.heroes[currentHero.name].costumes[costume.Data.type] = costume.Data;
                ConfigDataHelper.UserData = newData;
            };
        }

        UI_CostumeTab[] tabs = GetComponentsInChildren<UI_CostumeTab>();
        foreach (var tab in tabs)
        {
            ui_Tabs.Add(tab.type, tab);
            tab.OnSelected = OnTabSelected;
        }

        ui_Tabs[CostumeType.All].ToggleOn();

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
            ui_Item.OnSelected = OnItemSelected;
            ui_Item.UpdateContent(item);
            ui_Items.Add(ui_Item);
        }

        foreach (var costume in ConfigDataHelper.UserData.heroes[currentHero.name].costumes)
        {
            if (ui_Costumes.ContainsKey(costume.Key))
                ui_Costumes[costume.Key].UpDateContent(costume.Value);
        }
    }

    private void OnItemSelected(CostumeData costume)
    {
        Popup popup = PopupManager.Instance.GetPopup("EquipmentDetails");
        PopupEquipmentDetails details = popup.GetComponent<PopupEquipmentDetails>();
        details.UpdateContent(currentHero, costume);
        details.OnEquip = OnEquipCostume;
        details.OnUnEquip = OnUnEquip;
        popup.Show();
    }

    private void OnUnEquip(CostumeData data)
    {
        ui_Costumes[data.type].UpDateContent(null);

        UserData newData = (UserData)ConfigDataHelper.UserData.Clone();
        newData.heroes[currentHero.name].costumes.Remove(data.type);

        ConfigDataHelper.UserData = newData;

        UpdateAttributes();
    }

    private void OnEquipCostume(CostumeData costume)
    {
        ui_Costumes[costume.type].UpDateContent(costume);
        UpdateAttributes();
    }

    private void OnTabSelected(CostumeType type)
    {
        foreach (var item in ui_Items)
        {
            item.gameObject.SetActive(type == CostumeType.All || item.Data.type == type);
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
