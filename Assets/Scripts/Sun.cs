using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    [SerializeField] private float dropSpeed = 0.5f;
    private float timer = 0;
    [SerializeField] private float existTime = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > 0.5f)
        {
            Vector3 position = transform.position;
            position.y -= dropSpeed * Time.deltaTime;
            transform.position = position;
        }
        else
        {
            timer += Time.deltaTime;
            if (timer >= existTime)
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SunManager.getInstance().collectSun(25);
            Destroy(this.gameObject);
        }
    }
}
