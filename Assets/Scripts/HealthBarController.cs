using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    public GameObject healthBarPrefab;
    public Transform cam;
    Slider healthBar;

    // Start is called before the first frame update
    void Start()
    {
        GameObject obj = Instantiate(healthBarPrefab, transform);
        healthBar = obj.GetComponent<HealthBarCanvas>().slider;
        obj.GetComponent<HealthBarCanvas>().cam = cam;
    }

    // Update is called once per frame
    void Update()
    {
        Creature current = GetComponent<Creature>();
        healthBar.normalizedValue = current.healthPoint / current.maxHP;
    }
}
