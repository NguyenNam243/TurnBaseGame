using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    [Header("Map Configuration")]
    [SerializeField] private int levelIndex = 1;

    [Header("Object Reference")]
    [SerializeField] private HeroDispatcher heroDispatcher = null;


    private List<HeroController> enemies = null;

    private HeroController takingHero = null;
    private HeroController takedHero = null;


    private void Awake()
    {
        //enemies = heroDispatcher.SpawmMonster(levelIndex);


    }

}
