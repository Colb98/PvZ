using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureManager : MonoBehaviour
{
    public static CreatureManager instance;
    public List<Creature> creatures;

    public List<Plant> plantPrefabs;
    private int idCounter;

    public Dictionary<Type, List<Creature>> creaturePool;

    // Called before any Start() call, to create singleton
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("There is another instance of Creature Manager!");
        }

        instance = this;
        creatures = new List<Creature>();
        creaturePool = new Dictionary<Type, List<Creature>>();
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (Plant plant in plantPrefabs)
        {
            plant.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static CreatureManager GetInstance()
    {
        return instance;
    }

    public int OnNewCreature(Creature creature)
    {
        creatures.Add(creature);

        // Not thread-safe?
        idCounter++;

        return idCounter;
    }

    public void OnCreatureDead(Creature creature)
    {
        creatures.Remove(creature);
        creature.gameObject.SetActive(false);

        
        List<Creature> list = creaturePool.GetValueOrDefault(creature.GetType());
        if (!creaturePool.ContainsKey(creature.GetType()))
        {
            list = new List<Creature>();
            creaturePool.Add(creature.GetType(), list);
        }
        list.Add(creature);
        //Debug.Log("Add creature type " + creature.GetType());
    }

    public Creature GetOppositeCreatureInRowWithHitBox(int team, int row, float coordX, float hitBoxWidth)
    {
        foreach (Creature creature in creatures)
        {
            if (!creature.gameObject.activeSelf) continue;
            if (creature.team == team) continue;
            if (creature.GetHP() <= 0) continue;

            //TODO: creature width + shot direction
            float creatureWidth = 0.75f;
            bool isInRange = coordX - hitBoxWidth / 2 <= creature.coord.x + creatureWidth && coordX + hitBoxWidth / 2 >= creature.coord.x - creatureWidth;

            if (creature.coord.y == row && isInRange)
            {
                return creature;
            }
        }
        return null;
    }

    public Creature GetOppositeCreatureInRowInRange(Creature attacker)
    {
        return GetOppositeCreatureInRowInRange(attacker.team, (int)attacker.coord.y, attacker.coord.x, attacker.GetRangeDir());
    }

    public List<Creature> GetOppositeCreaturesInRowInRange(Creature attacker)
    {
        return GetOppositeCreaturesInRowInRange(attacker.team, (int)attacker.coord.y, attacker.coord.x, attacker.GetRangeDir());
    }

    public Creature GetOppositeCreatureInRowInRange(int team, int row, float coordX, float rangeDir)
    {
        foreach (Creature creature in creatures)
        {
            if (!creature.gameObject.activeSelf) continue;
            if (creature.team == team) continue;
            if (creature.GetHP() <= 0) continue;

            if (IsInRangeAndSameRow(creature, coordX, rangeDir, row))
            {
                return creature;
            }
        }
        return null;
    }

    public List<Creature> GetOppositeCreaturesInRowInRange(int team, int row, float coordX, float rangeDir)
    {
        List<Creature> ans = new List<Creature>();
        foreach (Creature creature in creatures)
        {
            if (!creature.gameObject.activeSelf) continue;
            if (creature.team == team) continue;
            if (creature.GetHP() <= 0) continue;

            if (IsInRangeAndSameRow(creature, coordX, rangeDir, row))
            {
                ans.Add(creature);
            }
        }
        return ans;
    }

    private bool IsInRangeAndSameRow(Creature creature, float coordX, float rangeDir, int row)
    {
        bool isInRange = false;
        if (rangeDir < 0)
        {
            isInRange = coordX + rangeDir <= creature.coord.x && coordX > creature.coord.x;
        }
        else
        {
            isInRange = coordX + rangeDir >= creature.coord.x && coordX < creature.coord.x;
        }
        if (creature.coord.y == row && isInRange)
        {
            return true;
        }
        return false;
    }

    internal static Plant AddPlantToTile(int plantIndex, Tile tile)
    {
        Plant ret = GetInstance().GetPlantByIndex(plantIndex);
        ret.transform.SetParent(tile.transform.parent);
        ret.gameObject.SetActive(true);
        ret.transform.position = tile.transform.position;
        ret.coord = tile.coord;

        return ret;
    }

    // Only return the prefabs
    public Plant GetPlantByIndex(int index)
    {
        return Instantiate(plantPrefabs[index]);
    }

    public int GetCost(int index)
    {
        return plantPrefabs[index].cost;
    }
}
