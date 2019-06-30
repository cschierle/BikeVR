using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusPoints : MonoBehaviour
{
    public GameObject flag;
    public GameObject halo;
    public PlayerController player;
    public MailboxFlag mailboxFlag;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Newspaper") && !mailboxFlag.up && halo.activeSelf)
        {
            player.score += 5;
            player.scoreText.text = "" + player.score;
            flag.transform.Rotate(new Vector3(0, 1, 0), -90);
            flag.transform.localPosition = new Vector3(flag.transform.localPosition.x - 0.25f, flag.transform.localPosition.y, flag.transform.localPosition.z + 0.25f);
            // light up in red to visualize bonus score
            halo.GetComponent<Light>().color = Color.red;
            mailboxFlag.up = true;
        }       
    }
}
