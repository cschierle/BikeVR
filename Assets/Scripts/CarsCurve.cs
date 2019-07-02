using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarsCurve : MonoBehaviour
{
    public GameObject go;
    public float speed;

    private bool start;
    private bool once;
    private int count;
    // Start is called before the first frame update
    void Start()
    {
        start = false;
        once = false;
        count = 90;
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSecondsRealtime(UnityEngine.Random.Range(1.0f, 8.0f));
        start = true;
        once = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            Vector3 movementVec = go.transform.forward * speed;
            go.GetComponent<Rigidbody>().MovePosition(go.GetComponent<Rigidbody>().position + movementVec);
            if (go.transform.position.x < 5.9 && count > 0)
            {
                go.transform.Rotate(new Vector3(0, 1, 0), -1f);
                count--;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EndFence")|| other.CompareTag("KillPlane"))
            Destroy(go);
        if(other.CompareTag("Cars")&& go.transform.rotation.y == 0)
        {
            start = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (once && other.CompareTag("Cars"))
        {
            StartCoroutine(Anfahren());
            print("colcurve");
        }
    }

    IEnumerator Anfahren()
    {
        yield return new WaitForSecondsRealtime(1f);
        start = true;
    }
}
