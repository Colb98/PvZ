using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
    public int id;
    public float maxHP;
    public float healthPoint;
    public float attackPoint;
    public float defendPoint;
    public float attackSpeed;


    public float attackTimer;
    public float timer;

    public int team;
    public float range;

    public Vector2 coord;


    private void Awake()
    {

    }

    // Start is called before the first frame update
    protected void Start()
    {

        id = CreatureManager.GetInstance().OnNewCreature(this);
        healthPoint = maxHP;
    }

    // Update is called once per frame
    protected void Update()
    {
        timer += Time.deltaTime;

        if (CanAttack())
        {
            attackTimer += Time.deltaTime;
            if (attackTimer >= attackSpeed)
            {
                attackTimer -= attackSpeed;
                Attack();
            }
        }
        else
        {
            attackTimer = attackSpeed;
        }

        coord.x = transform.position.x + 0.5f;
    }

    public virtual bool CanAttack()
    {
        return false;
    }

    public virtual void Attack()
    {
        //Debug.Log("Attack by object " + this.GetType().Name + " id: " + this.id);
    }

    public virtual Attack GetAttackPrefab()
    {
        return null;
    }

    public virtual float GetRangeDir()
    {
        return range;
    }

    public void ReceiveHealing(float healPoint)
    {
        healthPoint += healthPoint;
        if (healthPoint > maxHP)
        {
            healthPoint = maxHP;
        }
    }

    // TODO: input the Attack
    // Can even track to the attacker to do some reflect damage
    public void ReceiveDamage(float damage, Attack attack)
    {
        float realDamage = damage * GetDamageMultiplier() - GetDamageFlatReduction();
        if (realDamage < 0)
        {
            realDamage = 0.0f;
        }

        healthPoint -= realDamage;
        if (healthPoint <= 0)
        {
            healthPoint = 0;
            OnDeath();
        }
    }
    
    public void ReceiveDamage(float damage, Creature attacker)
    {
        float realDamage = damage * GetDamageMultiplier() - GetDamageFlatReduction();
        if (realDamage < 0)
        {
            realDamage = 0.0f;
        }

        healthPoint -= realDamage;
        if (healthPoint <= 0)
        {
            healthPoint = 0;
            OnDeath();
        }
    }

    public float GetDamageMultiplier()
    {
        return 1.0f;
    }

    public float GetDamageFlatReduction()
    {
        return GetDefendPoint();
    }

    protected virtual void OnDeath()
    {
        CreatureManager.GetInstance().OnCreatureDead(this);
        //gameObject.SetActive(false); // TODO: move to a pool 
    }

    public float GetDefendPoint()
    {
        return defendPoint;
    }
}
