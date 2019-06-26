using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailboxFlag : MonoBehaviour
{

    public GameObject go; //Flag
    public GameObject halo;

    private bool up;
    // Start is called before the first frame update
    void Start()
    {
        up = false;
        int random = new System.Random().Next(0,101);
        if (random % 2 == 0)
        {
            halo.SetActive(true);
            print("true");
        }
        else
        {
            halo.SetActive(false);
            print("false");
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !up && halo.activeSelf)
        {
            go.transform.Rotate(new Vector3(0, 1, 0), -90);
            go.transform.localPosition = new Vector3(go.transform.localPosition.x - 0.25f, go.transform.localPosition.y , go.transform.localPosition.z + 0.25f);
            halo.SetActive(false);
            up = true;
        }
    }
}
