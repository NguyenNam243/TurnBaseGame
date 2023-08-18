using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

    public static bool anyCardOn = false;
    public static Action<Tile> OnSpawnHeroEvent = null;

    private HeroController takingHero = null;
    private HeroController takedHero = null;

    private List<HeroController> takingGroup = new List<HeroController>();
    private List<HeroController> takedGroup = new List<HeroController>();


    private void Awake()
    {
        OnSpawnHeroEvent = OnSpawmHero;
        combatBtn.onClick.AddListener(OnStartCombat);

        takedGroup = SpawmMonster(levelIndex);
    }

    private void OnAttackFinish()
    {
        takingHero.hasAttack = true;

        List<HeroController> temp = takedGroup.Where(x => x.Alive).ToList();
        int targetIndex = UnityEngine.Random.Range(0, temp.Count);
        int index = 0;
        HeroController targetAttack = null;

        foreach (var hero in temp)
        {
            if (index == targetIndex)
            {
                targetAttack = hero;
                break;
            }

            index++;
        }

        if (targetAttack == null)
        {
            Debug.Log("End Combat");
            return;
        }

        HeroController nextHero = GetNextHero(takingGroup);
        if (nextHero != null)
        {
            nextHero.DoAttack(targetAttack);
            takingHero = nextHero;
        }
        else
        {
            ResetTurn();
            GameUtilities.Swap(ref takingGroup, ref takedGroup);
            takingHero = GetHeroMaxSpeed(takingGroup);
            takedHero = GetHeroMaxSpeed(takedGroup);
            takingHero.DoAttack(takedHero);
        }

    }

    private void ResetTurn()
    {
        foreach (var hero in takingGroup)
            hero.hasAttack = false;

        foreach (var hero in takedGroup)
            hero.hasAttack = false;
    }

    private HeroController GetNextHero(List<HeroController> collection)
    {
        foreach (var hero in takingGroup)
        {
            if (hero.hasAttack == false && hero.Alive)
            {
                return hero;
            }
        }
        return null;
    }

    public void OnSpawmHero(Tile tile)
    {
        HeroController hero = InstantiateHero(heroCardGroup.CurrentDataSelected.name, tile.transform.position, heroCardGroup.CurrentDataSelected);
        heroCardGroup.CurrentToggleOn.Hide();
        heroCardGroup.CurrentToggleOn.ToggleOff();
        tile.Hide();
        anyCardOn = false;
        takingGroup.Add(hero);
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
        HeroController heroMaxSpeed = GetHeroMaxSpeed(takingGroup);
        HeroController enemyMaxSpeed = GetHeroMaxSpeed(takedGroup);

        takingHero = heroMaxSpeed;
        takedHero = enemyMaxSpeed;

        if (heroMaxSpeed.HeroData.attributes[AttributeType.SPD].value < enemyMaxSpeed.HeroData.attributes[AttributeType.SPD].value)
        {
            GameUtilities.Swap(ref takingHero, ref takedHero);
            GameUtilities.Swap(ref takedGroup, ref takedGroup);
        }

        takingHero.DoAttack(takedHero);
    }

    private HeroController GetHeroMaxSpeed(List<HeroController> collection)
    {
        float maxSpeed = 0;
        HeroController maxHero = null;

        foreach (var hero in collection)
        {
            if (!hero.Alive)
                continue;

            if (hero.HeroData.attributes[AttributeType.SPD].value >= maxSpeed)
                maxHero = hero;
        }
        return maxHero;
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0) && heroCardGroup.AnyCardOn && heroCardGroup.CurrentDataSelected != null)
            anyCardOn = true;
    }
}
