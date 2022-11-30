using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunFlower : Plant
{
    //protected new void Start()
    //{
    //    base.Start();
    //    attac
    //}
    protected new void Update()
    {
        base.Update();
    }

    public override bool CanAttack()
    {
        return true;
    }

    public override void Attack()
    {
        Vector3 pos = transform.position;
        pos.y = 0.9f;
        SunManager.getInstance().DropSun(pos);
    }
}
