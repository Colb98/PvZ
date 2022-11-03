using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : Creature
{
    // Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}

    public override bool CanAttack()
    {
        if (StageZombieManager.GetInstance() == null) return false;

        return StageZombieManager.GetInstance().GetZombieCountInRow((int) coord.y) > 0;
    }
}
