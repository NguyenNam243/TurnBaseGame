using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAnimationUtilities : MonoBehaviour
{


    public Action OnAttack;
    public Action AttackFinish;


    public void OnAttackEvent()
    {
        OnAttack?.Invoke();
    }

    public void OnFinishAttackEvent()
    {
        AttackFinish?.Invoke();
    }
}
