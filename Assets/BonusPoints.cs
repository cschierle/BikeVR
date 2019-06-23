using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusPoints : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Newspaper")
        {
            // GameManager.score += 5
            Destroy(other.gameObject);
        }
    }
}
