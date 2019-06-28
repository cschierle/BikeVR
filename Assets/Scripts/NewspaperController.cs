using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewspaperController : MonoBehaviour
{
    public float despawnTimer;

    private void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(Despawn());
    }

    private IEnumerator Despawn()
    {
        yield return new WaitForSeconds(despawnTimer);
        Destroy(gameObject);
    }
}
