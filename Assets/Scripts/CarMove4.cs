using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CarMove4 : MonoBehaviour
{

    public int curves;
    public float speed;

    public GameObject car;

    private Boolean stop;
    private float angle;
    // Start is called before the first frame update
    void Start()
    {
        stop = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!stop)
        {
            Vector3 movementVec = car.transform.forward * speed;
            car.GetComponent<Rigidbody>().MovePosition(car.GetComponent<Rigidbody>().position + movementVec);
        }
        if (car.transform.position.z%100>46 && curves == 1)
        {
            StartCoroutine(Turn(90));
            curves = 2;
        }else if(car.transform.position.x > 12 && curves == 2)
        {
            StartCoroutine(Turn(-30));
            curves = 3;
        }
        else if (car.transform.position.x > 30 && curves == 3)
        {
            StartCoroutine(Turn(30));
            curves = 4;
        }
        else if (car.transform.position.x > 54 && curves == 4)
        {
            StartCoroutine(Turn(180));
            curves = 5;
        }
        else if (car.transform.position.x < 11 && curves == 5)
        {
            StartCoroutine(Turn(-180));
            curves = 6;
        }
        else if (car.transform.position.x > 54 && curves == 6)
        {
            StartCoroutine(Turn(180));
            curves = 7;
        }
        else if (car.transform.position.x < 30 && curves == 7)
        {
            StartCoroutine(Turn(30));
            curves = 8;
        }
        else if (car.transform.position.x < 14 && curves == 8)
        {
            StartCoroutine(Turn(-30));
            curves = 9;
        }
        else if (car.transform.position.x < -20 && curves == 9)
        {
            StartCoroutine(Turn(90));
            curves = 10;
        }
        else if(car.transform.position.z%100 < 40 && car.transform.position.z%100 > 38 && curves == 10)
        {
            StartCoroutine(Wait());
            curves = 1;
        }
    }

    IEnumerator Wait()
    {
        stop = true;
        yield return new WaitForSecondsRealtime(UnityEngine.Random.Range(1.0f, 2.5f));
        stop = false;
    }

    IEnumerator Turn(int v)
    {
        if (v == 180)
            angle = 1.7f;
        else
            angle = 2f;
        if(v>0)
            for(float i=v;i>0; i=i-angle)
            {
                car.transform.Rotate(new Vector3(0, 1, 0), angle);
                yield return new WaitForFixedUpdate();
            }
        else
            for (float i = v; i < 0; i=i+angle)
            {
                car.transform.Rotate(new Vector3(0, 1, 0), -angle);
                yield return new WaitForFixedUpdate();
            }
    }
}
