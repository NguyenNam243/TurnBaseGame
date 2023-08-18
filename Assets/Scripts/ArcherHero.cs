using DG.Tweening;
using UnityEngine;

public class ArcherHero : HeroController
{
    [Header("Object Reference")]
    [SerializeField] protected Transform bulletPos = null;

    public override string AttackAnimation => "Fire";


    protected override void DoAttackAnimation(HeroController target)
    {
        ator.Play(AttackAnimation);

        if (bulletObj == null)
            bulletObj = Resources.Load<GameObject>("Bullet");

        GameObject bulletPrefab = Instantiate(bulletObj);
        bulletPrefab.transform.position = bulletPos.position;
        bulletPrefab.SetActive(true);
        Bullet bullet = bulletPrefab.AddComponent<Bullet>();
        bullet.Fire(target.transform, () => 
        {
            ApplyDamage();
            DOVirtual.DelayedCall(1, () => { OnAttackFinish?.Invoke(); });
        });
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            DoAttack(targetAttack);
    }



}
