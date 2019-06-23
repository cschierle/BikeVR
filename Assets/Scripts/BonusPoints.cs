using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusPoints : MonoBehaviour
{
    public GameObject halo;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Newspaper")
        {
            // GameManager.score += 5
            Destroy(other.gameObject);
            halo.GetComponent<Light>().color = Color.red;
        }
    }
}
