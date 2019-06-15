using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotorMove : MonoBehaviour
{

    public GameObject go;
    public GameObject myPrefab;
    public float speed;
    public float turnspeed;

    private int i;
    private bool start;
    private bool once;
    private GameObject[] del;
    private GameObject delete;
    private Quaternion Rot;
    // Start is called before the first frame update
    void Start()
    {
        i = 2;
        start = false;
        once = true;
        Rot = go.transform.rotation;
    }

    // Update is called once per frame
    void Update() {     
        float movement = Input.GetAxis("Vertical");
        Vector3 movementVec = go.transform.forward * movement * speed * Time.deltaTime;
        go.GetComponent<Rigidbody>().MovePosition(go.GetComponent<Rigidbody>().position + movementVec);
        Turn();
    }

    void FixedUpdate()
    {
        if (go.transform.position.z % 100 < 10 && go.transform.position.z % 100 > -10 && once)
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
        if (go.transform.position.z % 100 < 60 && go.transform.position.z % 100 > 40)
        {
            once = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Respawn"))
        {
            go.transform.rotation = Rot;
            go.transform.position = new Vector3(0, go.transform.position.y, go.transform.position.z);
        }
    }

    void Turn()
    {
        float turn = Input.GetAxis("Horizontal");
        float angle = turn * turnspeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, angle, 0f);
        go.GetComponent<Rigidbody>().MoveRotation(go.GetComponent<Rigidbody>().rotation * turnRotation);
    }
}
