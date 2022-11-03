using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    public int id;
    // Damage initialize when the attack is created
    public float damage;
    public AttackType type;
    public TargetIdentity targetIdentity;
    public TargetType targetType;
    public bool dealtDamage;

    public float speed;

    // Only for reference
    public Creature creator;

    public List<Creature> affectedTargets;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        OnCreate();
    }

    public void OnCreate()
    {
        AttacksManager.GetInstance().OnAttackCreated(this);
        Reset();
    }

    public void Reset()
    {
        dealtDamage = false;
        affectedTargets = new List<Creature>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (!gameObject.activeSelf)
        {
            return;
        }

        // Projectile
        if (type == AttackType.Range)
        {
            // Default will fly left to right
            Vector3 location = transform.position;
            location.x += speed * Time.deltaTime;
            transform.position = location;
        }
    }

    // Base for projectile
    private void OnCollisionEnter(Collision collision)
    {
        if (dealtDamage) return;

        Creature creature = collision.gameObject.GetComponent<Creature>();
        //creature.ReceiveDamage(damage);
        affectedTargets.Add(creature);
        DoDamage();
        dealtDamage = true;

        AttacksManager.GetInstance().OnAttackEOL(this);
    }

    public abstract void DoDamage();

    public AttackType GetAttackType()
    {
        return type;
    }
}