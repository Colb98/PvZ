using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : Creature
{
    public int cost = 20;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        team = 0; // Plant default team zero
    }

    //// Update is called once per frame
    //void Update()
    //{

    //}
    public override float GetRangeDir()
    {
        return range;
    }

    public override bool CanAttack()
    {
        if (CreatureManager.GetInstance() == null) return false;

        Creature target = CreatureManager.GetInstance().GetOppositeCreatureInRowInRange(team, (int)coord.y, coord.x, GetRangeDir());
        return target != null;
    }
}
