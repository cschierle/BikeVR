using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarS2 : MonoBehaviour
{

    public GameObject go;
    public float speed;

    private bool start;
    // Start is called before the first frame update
    void Start()
    {
        start = true;
        StartCoroutine(Wait());
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            if (go.transform.position.x < -85 || go.transform.position.x > 70)
            {
                print(go + ";" +go.transform.position.x);
                go.transform.Rotate(new Vector3(0, 1, 0), 180f);
                if (go.transform.position.x < -80)
                    go.transform.position = go.transform.position + new Vector3(5, 0, 0);
                else
                    go.transform.position = go.transform.position + new Vector3(-5, 0, 0);
                    start = false;
                StartCoroutine(Wait());
            }

            Vector3 movementVec = go.transform.forward * speed;
            go.GetComponent<Rigidbody>().MovePosition(go.GetComponent<Rigidbody>().position + movementVec);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EndFence") || other.CompareTag("KillPlane"))
        {
            Destroy(go);
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSecondsRealtime(UnityEngine.Random.Range(1.0f, 4.0f));
        start = true;
    }
}
