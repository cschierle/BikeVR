using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuItemScript : MonoBehaviour
{
    public string SceneNameToLoad;
    public GameObject HighLight;
    public string Description;
    public Text TextOutput;

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
        HighLight.SetActive(true);
        TextOutput.text = Description;
    }

    public void GazeExit()
    {
        _gazeStatus = false;
        HighLight.SetActive(false);
        TextOutput.text = "";
    }
}
