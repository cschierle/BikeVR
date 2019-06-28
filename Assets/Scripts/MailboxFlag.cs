using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailboxFlag : MonoBehaviour
{

    public GameObject flag;
    public GameObject halo;
    public PlayerController player;

    private bool up;
    // Start is called before the first frame update
    void Start()
    {
        up = false;
        float random = UnityEngine.Random.Range(0.0f, 1.0f);
        if (random > 0.5f)
        {
            halo.SetActive(true);
        }
        else
        {
            halo.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Newspaper") && !up && halo.activeSelf)
        {
            // GameController.setScore(score += 1)
            // Destroy(other.gameObject);
            flag.transform.Rotate(new Vector3(0, 1, 0), -90);
            flag.transform.localPosition = new Vector3(flag.transform.localPosition.x - 0.25f, flag.transform.localPosition.y, flag.transform.localPosition.z + 0.25f);
            halo.SetActive(false);
            up = true;
        }
    }
}
