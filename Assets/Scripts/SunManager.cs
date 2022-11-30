using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunManager : MonoBehaviour
{
    [SerializeField] private int sun = 50;
    [SerializeField] private float sunDropRate = 10;
    [SerializeField] private GameObject sunPrefab;
    private float sunDropTimer = 0;
    private static SunManager _instance;
    
    public static SunManager getInstance()
    {
        return _instance;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (_instance != null)
        {
            Debug.Log("Planting Manager is inited!!");
            return;
        }
        _instance = this;
        //sun = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (sunDropRate > 0)
        {
            sunDropTimer += Time.deltaTime;
            while (sunDropTimer > sunDropRate)
            {
                DropSun();
                sunDropTimer -= sunDropRate;
            }
        }
    }

    public void DropSun()
    {
        MapGenerator mapGen = GetComponent<MapGenerator>();
        Vector3 coord = new Vector3(0, 0, 0);
        coord.x = Random.Range(0f, mapGen.width);
        coord.y = 2;
        coord.z = Random.Range(0f, mapGen.height) - mapGen.height / 2f;

        DropSun(coord);
    }

    public void DropSun(Vector3 coord)
    {
        MapGenerator mapGen = GetComponent<MapGenerator>();
        GameObject sun = Instantiate(sunPrefab, coord, sunPrefab.transform.rotation, mapGen.plane.transform);
    }

    public void collectSun(int num)
    {
        sun += num;
    }
    
    private bool useSun(int num)
    {
        if (sun < num)
        {
            return false;
        }
        sun -= num;
        return true;
    }

    public bool PlantAtTile(int plantIndex, Tile tile)
    {

        if (!tile.canBePlant)
        {
            Debug.Log("Cell can't plant");
            return false;
        }

        if (tile.curPlant != null)
        {
            Debug.Log("Plant invalid" + tile.curPlant.GetID());
            return false;
        }

        //Check if enough sun & reduce
        if (!useSun(CreatureManager.GetInstance().GetCost(plantIndex)))
        {
            Debug.Log("Not enough sun");
            return false;
        }

        Plant plant = CreatureManager.AddPlantToTile(plantIndex, tile);
        tile.Plant(plant);
        return true;
    }

    public int GetSun()
    {
        return sun;
    }
}
