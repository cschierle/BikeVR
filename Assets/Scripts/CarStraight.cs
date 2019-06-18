using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarStraight : MonoBehaviour
{

    public GameObject go;
    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        go.transform.position = go.transform.position + new Vector3(0, 0, -0.25f);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EndFence"))
            Destroy(go);
    }
}
