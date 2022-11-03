using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceShooterAttack : SingleTargetAttack
{
    //// Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}

    public override void DoSingleTargetDamage(Creature target)
    {
        base.DoSingleTargetDamage(target);
        MovementSlow effect = target.gameObject.AddComponent<MovementSlow>();
        effect.percentage = 0.3f;
        effect.duration = 1.0f;
    }
}
