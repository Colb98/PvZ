using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaShooter : Plant
{
    public SingleTargetAttack attackPrefab;

    private new void Start()
    {
        base.Start();
        attackPrefab.gameObject.SetActive(false); // TODO: move to pool/factory
    }

    public override Attack GetAttackPrefab()
    {
        return attackPrefab;
    }

    public override void Attack()
    {
        Vector3 position = transform.position;
        position.y = 0.55f;
        Attack projectile = AttacksManager.GetInstance().GetAttackOfType(attackPrefab.GetType(), this);
        projectile.transform.position = position;
        projectile.transform.SetParent(transform.parent);
        projectile.damage = GetAttackPoint();
        projectile.speed = 3;
        projectile.type = AttackType.Range;
    }

}
