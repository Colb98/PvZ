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
        base.Update();
    }

    public override void Attack()
    {
        AttacksManager.GetInstance().GetAttackOfType(null, this);
    }

    public override bool CanAttack()
    {
        if (CreatureManager.GetInstance() == null) return false;

        return CreatureManager.GetInstance().GetOppositeCreatureInRowInRange(this) != null;
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
