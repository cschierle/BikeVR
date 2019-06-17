using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailboxFlag : MonoBehaviour
{

    public GameObject go;

    private bool up;
    // Start is called before the first frame update
    void Start()
    {
        up = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !up)
        {
            go.transform.Rotate(new Vector3(0, 1, 0), -90);
            go.transform.localPosition = new Vector3(go.transform.localPosition.x - 0.25f, go.transform.localPosition.y , go.transform.localPosition.z + 0.25f);
            up = true;
        }
    }
}
