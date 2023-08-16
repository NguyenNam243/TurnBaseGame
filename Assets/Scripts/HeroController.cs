using System;
using DG.Tweening;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] protected float timeJump = 0.3f;
    [SerializeField] protected float jumpHeight = 1f;
    [SerializeField] protected float attackRadius = 1f;

    [Space]
    [Header("Object Reference")]
    [SerializeField] protected Transform bulletPos = null;

    public HeroController targetAttack = null;

    public HeroData HeroData { get; private set; }

    public virtual string AttackAnimation => "Attack";

    protected Vector3 defaultPos;
    protected Animator ator = null;
    protected HeroAnimationUtilities heroAnimation = null;

    protected bool canAttack = false;
    protected GameObject bulletObj = null;

    public bool Alive => CurrentHelth > 0;
    public float CurrentHelth { get; private set; }

    public Action OnAttackFinish = null;



    protected virtual void Awake()
    {
        ator = GetComponentInChildren<Animator>();
        heroAnimation = GetComponentInChildren<HeroAnimationUtilities>();

        heroAnimation.OnAttack = ApplyDamage;
    }


    public void Initialized(HeroData HeroData)
    {
        this.HeroData = HeroData;

        defaultPos = transform.position;
        CurrentHelth = HeroData.attributes[AttributeType.HP].value;
    }

    public virtual void DoAttack(HeroController target)
    {
        if (!Alive || !target.Alive)
            return;

        this.targetAttack = target;

        canAttack = CanAttack(target);

        DoAttackAnimation(target);
    }

    protected virtual void DoAttackAnimation(HeroController target)
    {

    }

    public virtual void ApplyDamage()
    {
        if (!canAttack)
            return;

        float applyDamage = HeroData.attributes[AttributeType.ATK].value;
        float targetDefenceDamage = targetAttack.GetDefence(applyDamage);
        float critDamage = GetCrit();
        float healingValue = GetHealing();

        applyDamage = applyDamage - targetDefenceDamage + critDamage;
        CurrentHelth += healingValue;


        targetAttack.TakedDamage(applyDamage);
    }

    public float GetDefence(float enemyDamage)
    {
        bool hasDefence = GameUtilities.GetRandomOnRange(0, HeroData.attributes[AttributeType.DFS].ratio);
        if (hasDefence)
        {
            float value = enemyDamage * (HeroData.attributes[AttributeType.DFS].value / 100f);
            Debug.Log($"{gameObject.name} xuất hiện bảo vệ: {value} damage");
            return value;
        }

        return 0;
    }

    public float GetCrit()
    {
        bool hasCrit = GameUtilities.GetRandomOnRange(0, HeroData.attributes[AttributeType.CRIT].ratio);
        if (hasCrit)
        {
            float value = HeroData.attributes[AttributeType.ATK].value * (HeroData.attributes[AttributeType.CRIT].value / 100f);
            Debug.Log($"{gameObject.name} xuất hiện CRIT: {value} damage");
            return value;
        }

        return 0;
    }

    public float GetHealing()
    {
        bool hasHealing = GameUtilities.GetRandomOnRange(0, HeroData.attributes[AttributeType.HEALING].ratio);
        if (hasHealing)
        {
            float value = HeroData.attributes[AttributeType.HP].value * (HeroData.attributes[AttributeType.HEALING].value / 100f);
            Debug.Log($"{gameObject.name} xuất hiện Healing: {value} HP");
            return value;
        }

        return 0;
    }

    public bool GetAccuracy()
    {
        return GameUtilities.GetRandomOnRange(0, HeroData.attributes[AttributeType.ACC].ratio);
    }

    protected bool CanAttack(HeroController target)
    {
        if (CurrentHelth <= 0 || target.CurrentHelth <= 0)
            return false;

        if (!GetAccuracy())
        {
            Debug.Log(gameObject.name + " đánh trượt");
            return false;
        }

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

        Debug.Log($"{gameObject.name} bị trừ {damage} HP - Còn lại: {CurrentHelth} HP");
    }

    protected Tween jumpTween = null;
    protected void MoveToRTarget(Transform target, Action OnFinish)
    {
        jumpTween.Stop();
        transform.DOJump(target.position + target.right * attackRadius, jumpHeight, 1, timeJump).OnComplete(() => { OnFinish?.Invoke(); });
    }

    protected void ReturnLocation(Action OnFinish)
    {
        jumpTween.Stop();
        transform.DOMove(defaultPos, timeJump).OnComplete(() => { OnFinish?.Invoke(); });
    }
}
