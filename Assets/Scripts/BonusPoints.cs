using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusPoints : MonoBehaviour
{
    public GameObject flag;
    public GameObject halo;
    public MailboxFlag mailboxFlag;
    public Text pointsText;

    private PlayerController player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Newspaper") && !mailboxFlag.up && halo.activeSelf)
        {
            pointsText.text = "+ 5";
            pointsText.color = Color.green;
            pointsText.GetComponentInParent<CanvasGroup>().alpha = 1f;
            player.UpdateScore(player.score + 5);
            flag.transform.Rotate(new Vector3(0, 1, 0), -90);
            flag.transform.localPosition = new Vector3(flag.transform.localPosition.x - 0.25f, flag.transform.localPosition.y, flag.transform.localPosition.z + 0.25f);
            // light up in green to visualize bonus score
            halo.GetComponent<Light>().color = Color.green;
            mailboxFlag.up = true;
        }       
    }
}
