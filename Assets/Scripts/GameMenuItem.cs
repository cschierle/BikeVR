using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenuItem : MonoBehaviour
{
    public bool GazedAt;
    
    void Start()
    {
        GazedAt = false;
    }

    public void GazeEnter()
    {
        GazedAt = true;
    }

    public void GazeExit()
    {
        GazedAt = false;
    }
}
