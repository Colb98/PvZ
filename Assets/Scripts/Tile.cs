using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Material normal;
    public Material bloom;

    public Plant curPlant;
    public bool canBePlant = true;

    public Vector2Int coord;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetNormal()
    {
        GetComponent<MeshRenderer>().material = normal;
    }

    public void SetGlow()
    {
        GetComponent<MeshRenderer>().material = bloom;
    }

    public void Plant(Plant plant)
    {
        curPlant = plant;
    }

    public void RemovePlant()
    {
        if (curPlant != null)
        {
            CreatureManager.GetInstance().OnCreatureDead(curPlant);
        }
        else
        {
            Debug.Log("No plant to remove!");
        }
    }
}
