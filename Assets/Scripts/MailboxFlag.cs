using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MailboxFlag : MonoBehaviour
{

    public GameObject flag;
    public GameObject halo;
    public Text pointsText;
    public bool TutorialLevel = false;

    private PlayerController player;
    private bool active;

    [HideInInspector] public bool up;

    private void Awake()
    {
        float random = UnityEngine.Random.Range(0.0f, 1.0f);
        if (random > 0.5f)
        {
            active = true;
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControlls>().UpdateMailboxes();
        }
        else
        {
            active = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!TutorialLevel)
        {
            up = false;
            float random = UnityEngine.Random.Range(0.0f, 1.0f);
            halo.SetActive(active);
        }
        else
        {
            up = false;
            halo.SetActive(true);
        }

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Newspaper") && !up && halo.activeSelf)
        {
            pointsText.text = "+ 1";
            pointsText.color = new Color(67f,87f,72f);
            pointsText.GetComponentInParent<CanvasGroup>().alpha = 1f;
            player.UpdateScore(player.score + 1);
            flag.transform.Rotate(new Vector3(0, 1, 0), -90);
            flag.transform.localPosition = new Vector3(flag.transform.localPosition.x - 0.25f, flag.transform.localPosition.y, flag.transform.localPosition.z + 0.25f);
            halo.SetActive(false);
            up = true;
        }
    }
}
