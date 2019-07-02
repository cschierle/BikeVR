using System.Collections;
using UnityEngine;
using UnityEngine.XR;

public class VRToggleScript : MonoBehaviour
{    
    void Start()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        Debug.Log("lol");
        StartCoroutine(LoadDevice("cardboard"));
#endif
    }

    private IEnumerator LoadDevice(string newDevice)
    {
        if(!XRSettings.loadedDeviceName.Equals("cardboard"))
        {
            XRSettings.LoadDeviceByName(newDevice);
            yield return null;
            XRSettings.enabled = true;
        }
    }
}
