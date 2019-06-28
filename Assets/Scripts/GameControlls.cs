using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControlls : MonoBehaviour
{

    public GameObject player;
    public GameObject scenePrefab;
    public GameObject newspaperPrefab;

    private int i;
    private bool start;
    private bool once;
    private GameObject[] del;
    private GameObject[] delcars;
    private GameObject delete;
    private GameObject fade;
    private GameObject fence;
    private Quaternion Rot;
    private PlayerController playerController;
    private int end;

    // Start is called before the first frame update
    void Start()
    {
        i = 2;
        start = false;
        once = true;
        Rot = player.transform.rotation;
        playerController = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player.transform.position.z % 100 < 10 && player.transform.position.z % 100 > -10 && once)
        {
            once = false;
            //new Object at i * 100 (vorrausschauend 2?)
            Instantiate(scenePrefab, new Vector3(0, 0, i * 100), Quaternion.identity);
            i++;
            if (start)
            {
                del = GameObject.FindGameObjectsWithTag("Scenes");
                for (int j = 0; j < del.Length; j++)
                {
                    if (del[j].transform.position.z + 50 < player.transform.position.z)
                    {
                        delete = del[j];
                        Destroy(delete);
                    }
                    if (del[j].transform.position.z < player.transform.position.z)
                    {
                        fence = del[j];
                        fence.transform.Find("Objects").Find("Fences").Find("EndFence").gameObject.SetActive(true);
                    }
                }
            }
            start = true;
        }

        if (player.transform.position.z % 100 < 60 && player.transform.position.z % 100 > 40)
        {
            once = true;
        }

        if (playerController.fading == 0 && InputManager.GetFire1ButtonDown())
        {
            // spawn and throw newspaper
            Transform newspaperSpawn = player.transform.Find("Main Camera").Find("NewspaperSpawn");
            Rigidbody newspaper = Instantiate(newspaperPrefab, newspaperSpawn.position, newspaperSpawn.rotation).GetComponent<Rigidbody>();
            newspaper.transform.Rotate(0f, 0f, 90f);
            // effect size of applied force controlled by newspaper's drag property in inspector
            newspaper.AddForce(newspaperSpawn.forward*1000);
        }
    }
}
