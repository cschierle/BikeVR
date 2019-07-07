using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsCanvasScript : MonoBehaviour
{
    Camera referenceCamera;
    
    public bool reverseFace = false;
    
    void Awake()
    {
        if (!referenceCamera)
            referenceCamera = Camera.main;
    }
    
    void LateUpdate()
    {
        // rotates the object relative to the camera
        Vector3 targetPos = transform.position + referenceCamera.transform.rotation * (reverseFace ? Vector3.forward : Vector3.back);
        Vector3 targetOrientation = referenceCamera.transform.rotation * Vector3.up;
        transform.LookAt(targetPos, targetOrientation);
    }
}
