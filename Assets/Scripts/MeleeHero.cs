using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class MeleeHero : HeroController
{
    private Tween backTween = null;

    protected override void Awake()
    {
        base.Awake();
        backTween.Stop();
        heroAnimation.AttackFinish = () =>
        {
            ReturnLocation(() =>
            {
                StartCoroutine(DelayCall(1, () =>
                {
                    OnAttackFinish?.Invoke();
                }));
            });
        };
    }

    private IEnumerator DelayCall(float time, Action Callback)
    {
        yield return new WaitForSeconds(time);
        Callback?.Invoke();
    }

    protected override void DoAttackAnimation(HeroController target)
    {
        MoveToRTarget(target.transform, () =>
        {
            ator.Play(AttackAnimation);
        });
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            DoAttack(targetAttack);
    }
}
