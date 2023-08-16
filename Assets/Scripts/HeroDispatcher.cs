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
    [Header("Map Configuration")]
    [SerializeField] private int levelIndex = 1;

    [Header("Component System")]
    [SerializeField] private HeroCardGroup heroCardGroup = null;

    [Header("Object Reference")]
    [SerializeField] private Button combatBtn = null;


    private List<HeroController> heroes = new List<HeroController>(); 
    private List<HeroController> enemies = null;

    public static bool anyCardOn = false;
    public static Action<Tile> OnSpawnHeroEvent = null;

    private HeroController takingHero = null;
    private HeroController takedHero = null;

    private Dictionary<HeroController, bool> takingGroup = new Dictionary<HeroController, bool>();
    private Dictionary<HeroController, bool> takedGroup = new Dictionary<HeroController, bool>();


    private void Awake()
    {
        OnSpawnHeroEvent = OnSpawmHero;
        combatBtn.onClick.AddListener(OnStartCombat);

        enemies = SpawmMonster(levelIndex);
    }

    private void OnAttackFinish()
    {
        Debug.Log("Next Turn");
        takingGroup[takingHero] = true;

        int targetIndex = UnityEngine.Random.Range(0, takedGroup.Count + 1);
        int index = 0;
        HeroController targetAttack = null;

        foreach (var hero in takedGroup)
        {
            if (index == targetIndex)
            {
                targetAttack = hero.Key;
                break;
            }
            index++;
        }

        foreach (var hero in takingGroup)
        {
            if (takingGroup[hero.Key] == false)
            {
                hero.Key.DoAttack(targetAttack);
                break;
            }
        }
    }

    public void OnSpawmHero(Tile tile)
    {
        HeroController hero = InstantiateHero(heroCardGroup.CurrentDataSelected.name, tile.transform.position, heroCardGroup.CurrentDataSelected);
        heroCardGroup.CurrentToggleOn.Hide();
        heroCardGroup.CurrentToggleOn.ToggleOff();
        tile.Hide();
        anyCardOn = false;
        heroes.Add(hero);
        hero.OnAttackFinish = OnAttackFinish;
    }

    public List<HeroController> SpawmMonster(int level)
    {
        List<HeroController> result = new List<HeroController>();
        LevelConfig levelConfig = ConfigDataHelper.GetLevelConfig(level);
        foreach (var monster in levelConfig.monsters)
        {
            HeroController _monster = InstantiateHero(monster.Value.heroData.name, new Vector3(monster.Value.position.x, monster.Value.position.y, monster.Value.position.z), monster.Value.heroData);
            _monster.transform.eulerAngles = new Vector3(0, 180, 0);
            result.Add(_monster);
            _monster.OnAttackFinish = OnAttackFinish;
        }
        return result;
    }

    private HeroController InstantiateHero(string heroName, Vector3 postition, HeroData heroData)
    {
        GameObject heroObj = Resources.Load<GameObject>(string.Format(GameConstants.HERO, heroName));
        GameObject obj = Instantiate(heroObj);
        HeroController heroController = obj.GetComponent<HeroController>();
        heroController.transform.position = postition;
        heroController.gameObject.SetActive(true);
        heroController.Initialized(heroData);
        return heroController;
    }

    private void OnStartCombat()
    {
        HeroController heroMaxSpeed = GetHeroMaxSpeed(heroes);
        HeroController enemyMaxSpeed = GetHeroMaxSpeed(enemies);

        takingHero = heroMaxSpeed;
        takedHero = enemyMaxSpeed;

        foreach (var hero in heroes)
            takingGroup.Add(hero, false);

        foreach (var hero in enemies)
            takedGroup.Add(hero, false);

        if (heroMaxSpeed.HeroData.attributes[AttributeType.SPD].value < enemyMaxSpeed.HeroData.attributes[AttributeType.SPD].value)
        {
            GameUtilities.Swap(ref takingHero, ref takedHero);
            GameUtilities.Swap(ref takedGroup, ref takedGroup);
        }

        takingHero.DoAttack(takedHero);
    }

    private HeroController GetHeroMaxSpeed(List<HeroController> collection)
    {
        float maxSpeed = collection[0].HeroData.attributes[AttributeType.SPD].value;
        int index = 0;

        for(int i = 0; i < collection.Count; i++)
        {
            if (collection[i].HeroData.attributes[AttributeType.SPD].value > maxSpeed)
            {
                maxSpeed = collection[i].HeroData.attributes[AttributeType.SPD].value;
                index = i;
            }
        }
        return collection[index];
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0) && heroCardGroup.AnyCardOn && heroCardGroup.CurrentDataSelected != null)
            anyCardOn = true;
    }
}
