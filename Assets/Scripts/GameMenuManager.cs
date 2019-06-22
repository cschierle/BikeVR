using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenuManager : MonoBehaviour
{
    public GameObject MenuCanvas;

    public List<Button> Buttons;

    private int numButtons;
    private int _selected;

    private bool _menuActive;
    private bool _isAxisInUse;

    void Start()
    {
        _menuActive = false;
        _isAxisInUse = false;
        MenuCanvas.SetActive(_menuActive);

        numButtons = Buttons.Count;
        _selected = 0;
    }

    void Update()
    {
        if (InputManager.GetRecenterButtonDown())
        {
            _menuActive = !_menuActive;
            _isAxisInUse = false;
            _selected = 0;
            MenuCanvas.SetActive(_menuActive);
            UpdateGUI();
        }

        if (_menuActive)
        {
            if (InputManager.GetAxis("Vertical") != 0)
            {
                if (!_isAxisInUse)
                {
                    if (InputManager.GetAxis("Vertical") > 0)
                    {
                        _selected = _selected > 0 ? _selected - 1 : numButtons - 1;
                        UpdateGUI();
                    }
                    else if (InputManager.GetAxis("Vertical") < 0)
                    {
                        _selected = _selected < numButtons - 1 ? _selected + 1 : 0;
                        UpdateGUI();
                    }
                    _isAxisInUse = true;
                }
            }
            else if (InputManager.GetAxis("Vertical") == 0)
            {
                _isAxisInUse = false;
            }

            if (InputManager.GetFire1ButtonDown())
            {
                Buttons[_selected].onClick.Invoke();
            }
        }

    }

    private void UpdateGUI()
    {
        for (int i = 0; i < numButtons; i++)
        {
            if (i == _selected)
            {
                Buttons[i].GetComponent<Image>().color = Color.red;
            }
            else
            {
                Buttons[i].GetComponent<Image>().color = Color.white;
            }
        }
    }

    public void CloseMenu()
    {
        if (_menuActive)
        {
            _menuActive = false;
            MenuCanvas.SetActive(false);
        }
    }

    public void ExitToMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
