using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarStraight : MonoBehaviour
{

    public GameObject go;
    public float speed;

    private bool start;
    private bool once;
    // Start is called before the first frame update
    void Start()
    {
        start = false;
        once = false;
        StartCoroutine(Wait());
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            go.transform.localPosition = go.transform.localPosition + new Vector3(0, 0, -speed);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (once)
            StartCoroutine(Anfahren());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EndFence")||other.CompareTag("KillPlane"))
        {
            Destroy(go);
        }

        if (other.CompareTag("Cars"))
        {
            start = false;
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSecondsRealtime(UnityEngine.Random.Range(1.0f, 8.0f));
        start = true;
        once = true;
    }

    IEnumerator Anfahren()
    {
        yield return new WaitForSecondsRealtime(1f);
        start = true;
    }
}
