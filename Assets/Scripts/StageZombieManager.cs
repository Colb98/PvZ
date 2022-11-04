using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageZombieManager : MonoBehaviour
{
    // Timeline timeline;
    public Zombie zombiePrefab;
    public Transform plane;
    public float time;
    private Timeline timeline;

    private static StageZombieManager instance;
    private Dictionary<int, List<Zombie>> zombieByRows;

    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        zombiePrefab.gameObject.SetActive(false);

        timeline = GetComponent<Timeline>();
        zombieByRows = new Dictionary<int, List<Zombie>>();

        if (instance == null)
        {
            instance = this;
        }
    }

    public static StageZombieManager GetInstance()
    {
        return instance;
    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;
        time += dt;

        int mapWidth = gameObject.GetComponent<MapGenerator>().width;
        int mapHeight = gameObject.GetComponent<MapGenerator>().height;

        List<Zombie> zombies = timeline.GetZombies(time, dt);
        foreach (var zombie in zombies)
        {
            int row = Mathf.FloorToInt(Random.value * mapHeight);
            InstantiateZombie(zombie, row);
        }
    }

    private void InstantiateZombie(Zombie prefab, int row)
    {

        int mapWidth = gameObject.GetComponent<MapGenerator>().width;
        int mapHeight = gameObject.GetComponent<MapGenerator>().height;

        Vector3 position = new Vector3(mapWidth, 0, -mapHeight / 2.0f + 0.5f + row);

        Zombie zombie = Instantiate(prefab, position, zombiePrefab.transform.rotation, plane);
        zombie.gameObject.SetActive(true);
        zombie.coord = new Vector2(0, row);

        List<Zombie> curRow = zombieByRows.GetValueOrDefault(row);
        if (!zombieByRows.ContainsKey(row))
        {
            curRow = new List<Zombie>();
            zombieByRows.Add(row, curRow);
        }
        curRow.Add(zombie);
    }

    bool PassThrough(float curTime, float dt, float value)
    {
        return curTime >= value && curTime - dt < value;
    }

    // TODO: Get zombie by distance (to check for range)

    public int GetZombieCountInRow(int row)
    {
        if (!zombieByRows.ContainsKey(row)) return 0;

        return zombieByRows.GetValueOrDefault(row).Count; 
    }

    public void OnZombieDeath(Zombie zombie)
    {
        List<Zombie> zombies = zombieByRows.GetValueOrDefault((int) zombie.coord.y);
        if (zombies == null)
        {
            Debug.LogError("Can't get zombie " + zombie.id + " at row: " + zombie.coord.y);
            return;
        }
        for (int i = 0; i < zombies.Count; i++)
        {
            if (zombies[i] == zombie)
            {
                zombies.RemoveAt(i);
                return;
            }
        }
    }
}
