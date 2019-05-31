using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotorMove : MonoBehaviour
{

    public GameObject go;
    public GameObject myPrefab;
    public float speed;
    private int i;
    private bool start;
    private bool once;
    private GameObject[] del;
    private GameObject delete;
    // Start is called before the first frame update
    void Start()
    {
        go.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, 100));
        i = 2;
        start = false;
        once = true;
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(0, 0, moveHorizontal);
        go.GetComponent<Rigidbody>().AddForce(movement * speed);
    }

    void FixedUpdate()
    {
        if (go.transform.position.z % 100 < 1 && go.transform.position.z % 100 > -1 && once)
        {
            once = false;
            //new Object at i * 100 (vorrausschauend 2?)
            Instantiate(myPrefab, new Vector3(0, 0, i * 100), Quaternion.identity);
            i++;
            if (start)
            {
                del = GameObject.FindGameObjectsWithTag("Scenes");
                for (int j = 0; j < del.Length; j++)
                {
                    if (del[j].transform.position.z + 50 < go.transform.position.z)
                    {
                        delete = del[j];
                    }
                }
                Destroy(delete);
            }
            start = true;
        }
        if (go.transform.position.z % 100 < 51 && go.transform.position.z % 100 > 49)
        {
            once = true;
        }
    }
}
