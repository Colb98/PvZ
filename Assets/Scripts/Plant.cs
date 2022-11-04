using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : Creature
{
    // Start is called before the first frame update
    private new void Start()
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

        return CreatureManager.GetInstance().HasOppositeCreatureInRowInRange(team, (int)coord.y, coord.x, GetRangeDir());
    }
}
