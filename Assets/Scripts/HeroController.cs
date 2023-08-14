using System;
using DG.Tweening;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class HeroController : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private float timeJump = 0.3f;
    [SerializeField] private float jumpHeight = 1f;
    [SerializeField] private float attackRadius = 1f;

    [Space]
    [Header("Object Reference")]
    [SerializeField] private Transform bulletPos = null;

    public HeroController targetAttack = null;

    public HeroData HeroData { get; private set; }

    private Vector3 defaultPos;
    private Animator ator = null;
    private HeroAnimationUtilities heroAnimation = null;

    private GameObject bulletObj = null;

    public float CurrentHelth { get; private set; }



    private void Awake()
    {
        ator = GetComponentInChildren<Animator>();
        heroAnimation = GetComponentInChildren<HeroAnimationUtilities>();

        heroAnimation.OnAttack = ApplyDamage;
        heroAnimation.AttackFinish = () => {
            ReturnLocation(() =>
            {
                // Next turn
            });
        };
    }


    public void Initialized(HeroData HeroData)
    {
        this.HeroData = HeroData;

        defaultPos = transform.position;
        CurrentHelth = HeroData.attributes[AttributeType.HP].value;
    }

    public void DoAttack(HeroController target)
    {
        this.targetAttack = target;

        bool canAttack = CanAttack(target);
        if (!canAttack)
            return;

        MoveToRTarget(target.transform, () => 
        {
            ator.Play("Attack");
        });
    }

    public void DoFire(HeroController target)
    {
        ator.Play("Fire");

        if (bulletObj == null)
            bulletObj = Resources.Load<GameObject>("Bullet");

        GameObject bulletPrefab = Instantiate(bulletObj);
        bulletPrefab.transform.position = bulletPos.position;
        bulletPrefab.SetActive(true);
        Bullet bullet = bulletPrefab.AddComponent<Bullet>();
        bullet.Fire(target.transform);
    }

    private void ApplyDamage()
    {
        float applyDamage = HeroData.attributes[AttributeType.ATK].value;
        float targetDefenceDamage = targetAttack.GetDefence();
        float critDamage = GetCrit();
        float healingValue = GetHealing();

        applyDamage = applyDamage - targetDefenceDamage + critDamage;
        CurrentHelth += healingValue;

        targetAttack.TakedDamage(applyDamage);
    }

    public float GetDefence()
    {
        bool hasDefence = GameUtilities.GetRandomOnRange(0, HeroData.attributes[AttributeType.DFS].ratio);
        if (hasDefence)
            return HeroData.attributes[AttributeType.DFS].value;
        return 0;
    }

    public float GetCrit()
    {
        bool hasCrit = GameUtilities.GetRandomOnRange(0, HeroData.attributes[AttributeType.CRIT].ratio);
        if (hasCrit)
            return HeroData.attributes[AttributeType.ATK].value * (HeroData.attributes[AttributeType.CRIT].value / 100f);
        return 0;
    }

    public float GetHealing()
    {
        bool hasHealing = GameUtilities.GetRandomOnRange(0, HeroData.attributes[AttributeType.HEALING].ratio);
        if (hasHealing)
            return HeroData.attributes[AttributeType.HP].value * (HeroData.attributes[AttributeType.HEALING].value / 100f);
        return 0;
    }

    public bool GetAccuracy()
    {
        return GameUtilities.GetRandomOnRange(0, HeroData.attributes[AttributeType.ACC].ratio);
    }

    private bool CanAttack(HeroController target)
    {
        if (CurrentHelth <= 0 || target.CurrentHelth <= 0)
            return false;

        if (!GetAccuracy())
            return false;

        return true;
    }

    public void TakedDamage(float damage)
    {
        ator.Play("Hit");
        CurrentHelth -= damage;
        if (CurrentHelth <= 0)
        {
            CurrentHelth = 0;
            ator.Play("Die");
        }
    }

    private Tween jumpTween = null;
    private void MoveToRTarget(Transform target, Action OnFinish)
    {
        jumpTween.Stop();
        transform.DOJump(target.position + target.right * attackRadius, jumpHeight, 1, timeJump).OnComplete(() => { OnFinish?.Invoke(); });
    }

    private void ReturnLocation(Action OnFinish)
    {
        jumpTween.Stop();
        transform.DOMove(defaultPos, timeJump).OnComplete(() => { OnFinish?.Invoke(); });
    }
}
