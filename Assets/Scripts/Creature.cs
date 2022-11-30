using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
    [SerializeField] protected int id;
    [SerializeField] protected float maxHP;
    [SerializeField] protected float healthPoint;
    [SerializeField] protected float attackPoint;
    [SerializeField] protected float defendPoint;
    [SerializeField] protected float attackSpeed;
    [SerializeField] protected Sprite avatar;

    [SerializeField] protected float attackTimer;
    [SerializeField] protected float timer;
    [SerializeField] protected bool showDebug = false;

    public int team;
    public float range;

    public Vector2 coord;
    public bool isAOE = false;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    protected virtual void Start()
    {

        id = CreatureManager.GetInstance().OnNewCreature(this);
        healthPoint = maxHP;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        timer += Time.deltaTime;

        bool canAttack = CanAttack();
        if (canAttack || !IsAttackCooledDown())
        {
            attackTimer += Time.deltaTime;

            if (canAttack && IsAttackCooledDown())
            {
                attackTimer -= attackSpeed;
                Attack();
            }
        }

        coord.x = transform.position.x + 0.5f;
    }

    public bool IsAttackCooledDown()
    {
        return attackTimer >= attackSpeed;
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
        return defendPoint;
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

    public int GetID()
    {
        return id;
    }

    public float GetHP()
    {
        return healthPoint;
    }
    

    public float GetMaxHP()
    {
        return maxHP;
    }

    public Sprite GetAvatar()
    {
        return avatar;
    }

    public float GetAttackSpeed()
    {
        return attackSpeed;
    }

    public float GetAttackPoint()
    {
        return attackPoint;
    }
}
