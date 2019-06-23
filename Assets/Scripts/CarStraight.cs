using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarStraight : MonoBehaviour
{

    public GameObject go;
    public float speed;
    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        go.transform.localPosition = go.transform.localPosition + new Vector3(0, 0, -speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EndFence")||other.CompareTag("KillPlane"))
        {
            Destroy(go);
        }
            
    }
}
