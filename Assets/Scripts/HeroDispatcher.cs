using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HeroDispatcher : MonoBehaviour
{
    [Header("Component System")]
    [SerializeField] private HeroCardGroup heroCardGroup = null;


    public static bool anyCardOn = false;
    public static Action OnSpawnHeroEvent = null;


    private void Awake()
    {
        OnSpawnHeroEvent = OnSpawmHero;
    }

    private void OnSpawmHero()
    {
        heroCardGroup.CurrentToggleOn.ToggleOff();
        anyCardOn = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0) && heroCardGroup.AnyCardOn && heroCardGroup.CurrentDataSelected != null)
            anyCardOn = true;
    }
}
