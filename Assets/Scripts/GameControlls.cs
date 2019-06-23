using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControlls : MonoBehaviour
{

    public GameObject go;
    public GameObject myPrefab;

    private int i;
    private bool start;
    private bool once;
    private GameObject[] del;
    private GameObject[] delcars;
    private GameObject delete;
    private GameObject fade;
    private GameObject fence;
    private Quaternion Rot;
    private int fading;
    private int end;
    // Start is called before the first frame update
    void Start()
    {
        i = 2;
        start = false;
        once = true;
        Rot = go.transform.rotation;
        fading = 0;
    }

    // Update is called once per frame
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
                        Destroy(delete);
                    }
                    if (del[j].transform.position.z < go.transform.position.z)
                    {
                        fence = del[j];
                        fence.transform.Find("Objects").Find("Fences").Find("EndFence").gameObject.SetActive(true);
                    }
                }

            }
            start = true;
            delcars = GameObject.FindGameObjectsWithTag("Cars");
            for (int j = 0; j < delcars.Length; j++)
            {
                if (delcars[j].transform.position.z < i - 3 * 100)
                {
                    delete = delcars[j];
                    Destroy(delete);
                }
            }
        }
        if (go.transform.position.z % 100 < 60 && go.transform.position.z % 100 > 40)
        {
            once = true;
        }
    }
}
