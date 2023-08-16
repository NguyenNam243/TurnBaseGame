using DG.Tweening;
using UnityEngine;

public class MeleeHero : HeroController
{

    protected override void Awake()
    {
        base.Awake();
        heroAnimation.AttackFinish = () =>
        {
            ReturnLocation(() =>
            {
                DOVirtual.DelayedCall(1, () => 
                {
                    OnAttackFinish?.Invoke();
                });
            });
        };
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
