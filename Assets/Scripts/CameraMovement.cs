using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Camera cam;
    public float turnSpeed;

    private Vector3 mouseOrigin;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        mouseOrigin = Input.mousePosition;
        Vector3 pos = cam.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);

        cam.transform.RotateAround(transform.position, transform.right, -pos.y * turnSpeed);
        cam.transform.RotateAround(transform.position, Vector3.up, pos.x * turnSpeed);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        mouseOrigin = Input.mousePosition;
        Vector3 pos = cam.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);

        cam.transform.RotateAround(transform.position, transform.right, -pos.y * turnSpeed);
        cam.transform.RotateAround(transform.position, Vector3.up, pos.x * turnSpeed);

    }
}
