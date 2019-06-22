using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputManager
{
    public static float GetAxis(string orientation)
    {
#if UNITY_EDITOR
        return Input.GetAxis(orientation);
#elif UNITY_ANDROID
        if (orientation.Equals("Horizontal", System.StringComparison.InvariantCultureIgnoreCase))
        {
            return Input.GetAxis("Vertical");
        }

        return Input.GetAxis("Horizontal");
#endif
    }

    public static bool GetRecenterButtonDown()
    {
#if UNITY_EDITOR
        return Input.GetButtonDown("Jump");
        //return false;
#elif UNITY_ANDROID
        return (Input.GetKeyDown("joystick 1 button 2"));
#endif
    }

    public static bool GetRecenterButton()
    {
#if UNITY_EDITOR
        return Input.GetButton("Jump");
        //return false;
#elif UNITY_ANDROID
        return (Input.GetKey("joystick 1 button 2"));
#endif
    }

    public static bool GetRecenterButtonUp()
    {
#if UNITY_EDITOR
        return Input.GetButtonUp("Jump");
        //return false;
#elif UNITY_ANDROID
        return (Input.GetKeyUp("joystick 1 button 2"));
#endif
    }

    public static bool GetFire1ButtonDown()
    {
#if UNITY_EDITOR
        return Input.GetButtonDown("Fire1");
#elif UNITY_ANDROID
        return (Input.GetKeyDown("joystick 1 button 4"));
#endif
    }
}
