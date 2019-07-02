using System.Collections;
using UnityEngine;
using UnityEngine.XR;

public class VRToggleScript : MonoBehaviour
{
    public bool ToggleVRMode = false;
    
    void Start()
    {
        if (ToggleVRMode)
        {
            StartCoroutine(LoadDevice("cardboard"));
        }
        else
        {
            StartCoroutine(LoadDevice("None"));
        }
    }

    private IEnumerator LoadDevice(string newDevice)
    {
        XRSettings.LoadDeviceByName(newDevice);
        yield return null;
        XRSettings.enabled = true;
    }
}
