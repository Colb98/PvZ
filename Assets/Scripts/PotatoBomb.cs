using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotatoBomb : Plant
{
    public float prepareTimer;
    public float prepareTime = 2f;
    protected new void Start()
    {
        base.Start();
        isAOE = true;
        transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        prepareTimer = 0.0f;
    }

    protected override void Update()
    {
        base.Update();

        bool isReady = IsReady();
        prepareTimer += Time.deltaTime;
        if (!isReady && IsReady())
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    public override bool CanAttack()
    {
        if (!IsReady()) return false;

        return base.CanAttack();
    }

    public bool IsReady()
    {
        return prepareTimer >= prepareTime;
    }

    public override void Attack()
    {
        Attack projectile = AttacksManager.GetInstance().GetAttackOfType(null, this);
        OnDeath();
    }
}
