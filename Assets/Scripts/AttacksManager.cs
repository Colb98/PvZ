using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttacksManager : MonoBehaviour
{
    static AttacksManager instance;

    Dictionary<int, Attack> attacks;

    // The attack pool
    public Dictionary<Type, List<Attack>> pool;

    int attackCounter;

    private void Awake()
    {
        if (instance != null)
        {
            throw new System.Exception("There are more than 1 Attacks Manager");
        }

        instance = this;
        attackCounter = 0;
        attacks = new Dictionary<int, Attack>();
        pool = new Dictionary<Type, List<Attack>>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public static AttacksManager GetInstance()
    {
        return instance;
    }
    
    public Attack GetAttackOfType(Type type, Attack prefab)
    {
        if (!pool.ContainsKey(type))
        {
            pool[type] = new List<Attack>();
        }

        List<Attack> attackOfType = pool[type];

        Attack ret;
        if (attackOfType.Count > 0)
        {
            //Debug.Log("Get attack of type " + type + " hit");
            ret = attackOfType[0];
            attackOfType.RemoveAt(0);
            ret.OnCreate();
        }
        else
        {
            //Debug.Log("Get attack of type " + type + " miss");
            ret = Instantiate(prefab);
        }
        ret.gameObject.SetActive(true);
        return ret;
    }

    public void OnAttackCreated(Attack attack)
    {
        attack.id = attackCounter++;

        attacks[attack.id] = attack;
    }

    public void OnAttackEOL(Attack attack)
    {
        attacks.Remove(attack.id);
        if (!pool.ContainsKey(attack.GetType()))
        {
            pool.Add(attack.GetType(), new List<Attack>());
        }
        pool[attack.GetType()].Add(attack);

        attack.gameObject.SetActive(false);
    }
}
