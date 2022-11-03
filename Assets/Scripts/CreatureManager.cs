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
        Debug.Log("Add creature type " + creature.GetType());
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
}
