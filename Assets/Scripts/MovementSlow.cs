using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSlow : CreatureStatusEffect
{
    public float percentage;
    //// Start is called before the first frame update
    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}

    public override float GetSpeedMultiplier()
    {
        if (host.GetType() == typeof(Zombie))
        {
            return -percentage;
        }
        return 0f;
    }
}
