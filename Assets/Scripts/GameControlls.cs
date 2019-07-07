using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControlls : MonoBehaviour
{

    public GameObject player;
    public GameObject scenePrefab;
    public GameObject scenePrefab2;
    public GameObject scenePrefab3;
    public GameObject scenePrefab4;

    public GameObject sceneStart;
    public GameObject sceneFinish;

    public GameObject endScreen;
    public Text endScreenStats;

    public int length;

    [HideInInspector] public int mailboxes;
    [HideInInspector] public int respawns;

    private int k;
    private float newspaper;
    private bool start;
    private bool once;
    private GameObject[] del;
    private GameObject[] delcars;
    private GameObject delete;
    private GameObject fade;
    private GameObject fence;
    private GameObject scene;
    private Quaternion Rot;
    private PlayerController playerController;
    private int end;

    // Start is called before the first frame update
    void Start()
    {
        k = 2;
        mailboxes = 0;
        newspaper = 0;
        respawns = 0;
        start = false;
        once = true;
        Rot = player.transform.rotation;
        playerController = player.GetComponent<PlayerController>();
        Init();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player.transform.position.z % 100 < 10 && player.transform.position.z % 100 > -10 && once)
        {
            if (k < length)
            {
                once = false;
                //new Object at i * 100 (vorrausschauend 2?)
                RandomScene();

                Instantiate(scene, new Vector3(0, 0, k * 100), Quaternion.identity);
                k++;
                print(k);
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
            else
            {

                Instantiate(sceneFinish, new Vector3(0.75f, 8.35f, (k * 100) - 7.2f), Quaternion.Euler(0, 180, 0));// *Quaternion.identity);
                once = false;
                k++;
            }
        }

        if (player.transform.position.z % 100 < 60 && player.transform.position.z % 100 > 40)
        {
            if(k<length+1)
                once = true;
        }
    }

    private void Init()
    {
        for(int i = 0; i<2; i++)
        {
            if(i==0)
                Instantiate(sceneStart, new Vector3(-0.45f, 10.24f, -7), Quaternion.identity);
            RandomScene();
            Instantiate(scene, new Vector3(0, 0, i * 100), Quaternion.identity);
            /*if (i == 0)
            {
                fence = GameObject.FindGameObjectWithTag("Scenes");
                fence.transform.Find("Objects").Find("Fences").Find("EndFence").gameObject.SetActive(true);
            }*/
        } 
    }

    private void RandomScene()
    {

        float random = UnityEngine.Random.Range(0.0f, 4.0f);
        if (random < 1)
        {
            scene = scenePrefab;
        }
        else if (random < 2)
        {
            scene = scenePrefab2;
        }
        else if (random < 3)
        {
            scene = scenePrefab3;
        }
        else if (random <= 4)
        {
            scene = scenePrefab4;
        }

    }

    public void UpdateMailboxes()
    {
        mailboxes++;
    }

    public void UpdateNewspaper()
    {
        newspaper++;
    }

    public void DisplayEndScreen(int score, float delivered)
    {
        endScreen.SetActive(true);
        float acc = (delivered/newspaper) *100;
        endScreenStats.text = "Points: " + score + "\n Respawns: " + respawns + "\n \n Delivered Mailboxes: " + delivered + " / " + mailboxes + "\n Thrown Newspapers: " + newspaper + "\n Accuracy: " + acc + "%";
    }
}
