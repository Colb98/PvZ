using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleTargetAttack : Attack
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        targetType = TargetType.Single;
    }

    // Update is called once per frame

    protected override void Update()
    {
        base.Update();
    }


    public override void DoDamage()
    {
        foreach(Creature creature in affectedTargets)
        {
            DoSingleTargetDamage(creature);
        }
    }

    public virtual void DoSingleTargetDamage(Creature target)
    {
        target.ReceiveDamage(damage, this);
    }
}
