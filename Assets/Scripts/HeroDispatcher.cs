using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using TB.Grid;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HeroDispatcher : MonoBehaviour
{
    [Header("Component System")]
    [SerializeField] private HeroCardGroup heroCardGroup = null;

    [Header("Object Reference")]
    [SerializeField] private Button combatBtn = null;


    public static bool anyCardOn = false;
    public static Action<Tile> OnSpawnHeroEvent = null;


    private void Awake()
    {
        OnSpawnHeroEvent = OnSpawmHero;
        combatBtn.onClick.AddListener(OnStartCombat);

        SpawmMonster(1);
    }

    private void OnSpawmHero(Tile tile)
    {
        InstantiateHero("Hero", tile.transform.position, heroCardGroup.CurrentDataSelected);
        heroCardGroup.CurrentToggleOn.Hide();
        heroCardGroup.CurrentToggleOn.ToggleOff();
        tile.Hide();
        anyCardOn = false;
    }

    private void SpawmMonster(int level)
    {
        LevelConfig levelConfig = ConfigDataHelper.GetLevelConfig(level);
        foreach (var monster in levelConfig.monsters)
        {
            InstantiateHero("Hero", new Vector3(monster.Value.position.x, monster.Value.position.y, monster.Value.position.z), monster.Value.heroData);
        }
    }

    private void InstantiateHero(string heroName, Vector3 postition, HeroData heroData)
    {
        GameObject heroObj = Resources.Load<GameObject>(string.Format(GameConstants.HERO, heroName));
        HeroController heroController = Instantiate(heroObj, transform).GetComponent<HeroController>();
        heroController.transform.position = postition;
        heroController.gameObject.SetActive(true);
        heroController.Initialized(heroData);
    }

    private void OnStartCombat()
    {

    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0) && heroCardGroup.AnyCardOn && heroCardGroup.CurrentDataSelected != null)
            anyCardOn = true;
    }
}
