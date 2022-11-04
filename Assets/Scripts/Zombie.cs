using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Creature
{
    public float movementSpeed;

    // Start is called before the first frame update
    protected new void Start()
    {
        base.Start();
        team = 1;
    }

    // Update is called once per frame
    protected new void Update()
    {
        base.Update();
        if (transform.position.x < -0.5)
        {
            gameObject.SetActive(false);
            return;
        }
        float dt = Time.deltaTime;
        if (!CanAttack())
        {
            transform.Translate(new Vector3(-GetMovementSpeed() * dt, 0, 0), Space.World);
        }
    }

    public override void Attack()
    {
        Debug.Log("Zombie id " + id + " attack bam bam");
    }

    public override bool CanAttack()
    {
        if (CreatureManager.GetInstance() == null) return false;

        return CreatureManager.GetInstance().HasOppositeCreatureInRowInRange(team, (int)coord.y, coord.x, GetRangeDir());
    }
    protected override void OnDeath()
    {
        base.OnDeath();
        StageZombieManager.GetInstance().OnZombieDeath(this);
    }

    public override float GetRangeDir()
    {
        return -range;
    }

    public float GetMovementSpeed()
    {
        float ret = movementSpeed;
        List<CreatureStatusEffect> effects = new List<CreatureStatusEffect>(GetComponents<CreatureStatusEffect>());

        float buffMax = 0.0f;
        float debuffMax = 0.0f;

        foreach (CreatureStatusEffect effect in effects){
            float multiplier = effect.GetSpeedMultiplier();
            bool isDebuff = multiplier < 0f;
            if (isDebuff && debuffMax > multiplier)
            {
                debuffMax = multiplier;
            }
            else if (!isDebuff && buffMax < multiplier)
            {
                buffMax = multiplier;
            }
        }

        ret *= 1.0f + debuffMax + buffMax;
        return ret;
    }
}
