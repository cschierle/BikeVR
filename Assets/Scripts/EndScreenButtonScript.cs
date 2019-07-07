using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenButtonScript : MonoBehaviour
{
    public string SceneNameToLoad;
    private bool _gazeStatus;
    
    void Start()
    {
        _gazeStatus = false;
    }
    
    void Update()
    {
        if (InputManager.GetFire1ButtonDown() && _gazeStatus)
        {
            SceneManager.LoadScene(SceneNameToLoad);
        }
    }

    public void GazeEnter()
    {
        _gazeStatus = true;
    }

    public void GazeExit()
    {
        _gazeStatus = false;
    }
}
