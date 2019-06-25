using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanePos : MonoBehaviour
{
    public GameObject go;
    public GameObject bike;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        go.transform.position = bike.transform.position + new Vector3(0, 0, -100);
    }
}
