using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPositioner : MonoBehaviour
{
    [SerializeField] private MapGenerator mapGenerator;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 position = transform.position;
        position.x = (mapGenerator.width + 0.5f) / 2f;
        transform.position = position;
        transform.LookAt(new Vector3((mapGenerator.width + 0.5f) / 2f, 0.0f, 0.0f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
