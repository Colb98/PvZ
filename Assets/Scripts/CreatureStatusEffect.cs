using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureStatusEffect : MonoBehaviour
{
    public Creature host;
    public float duration;

    // Start is called before the first frame update
    void Start()
    {
        host = gameObject.GetComponent<Creature>();
    }

    // Update is called once per frame
    void Update()
    {
        duration -= Time.deltaTime;
        if (duration < 0)
        {
            Destroy(this);
        }
    }

    public virtual float GetMaxHP()
    {
        return host.maxHP;
    }

    public virtual float GetHP()
    {
        return host.maxHP;
    }

    public virtual float GetSpeed()
    {
        if (host.GetType() == typeof(Zombie))
        {
            return ((Zombie)host).movementSpeed;
        }
        return 0f;
    }

    public virtual float GetAttackSpeed()
    {
        return host.attackSpeed;
    }

    public virtual float GetAttackPoint()
    {
        return host.attackPoint;
    }

    public virtual float GetDefendPoint()
    {
        return host.defendPoint;
    }

    public virtual float GetSpeedMultiplier()
    {
        return 0.0f;
    }
}
