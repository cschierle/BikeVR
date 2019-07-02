using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenuManager : MonoBehaviour
{
    public Button Button1;
    public Button ExitButton;
    public GameObject MainPanel;
    public GameObject ConfirmPanel;
    public Text ConfirmText;
    public Text DelieveredAmount;

    private MenuStates menuState;
    private FirstSelection firstSelection;
    private Button yesButton;
    private Button noButton;

    public enum MenuStates
    {
        MainPanel,
        ConfirmPanel
    }

    public enum FirstSelection
    {
        Button1,
        ExitGame
    }

    void Start()
    {
        if (SceneManager.GetActiveScene().name.Equals("TutorialScene"))
        {
            Button1.gameObject.SetActive(true);
            Button1.GetComponentInChildren<Text>().text = "Proceed to Game";
        }

        yesButton = ConfirmPanel.GetComponentsInChildren<Button>().FirstOrDefault(c => c.tag.Equals("YesButton"));
        noButton = ConfirmPanel.GetComponentsInChildren<Button>().FirstOrDefault(c => c.tag.Equals("NoButton"));

        DelieveredAmount.text = "0";
        menuState = MenuStates.MainPanel;

        Button1.onClick.AddListener(Button1OnClick);
        ExitButton.onClick.AddListener(ExitGameOnClick);

        yesButton.onClick.AddListener(() => ConfirmSelection(true));
        noButton.onClick.AddListener(() => ConfirmSelection(false));
    }

    void Update()
    {
        switch (menuState)
        {
            case MenuStates.MainPanel:
                {
                    MainPanel.SetActive(true);
                    ConfirmPanel.SetActive(false);
                    break;
                }
            case MenuStates.ConfirmPanel:
                {
                    MainPanel.SetActive(false);
                    ConfirmPanel.SetActive(true);

                    switch (firstSelection)
                    {
                        case FirstSelection.Button1:
                            {
                                if (SceneManager.GetActiveScene().name.Equals("TutorialScene"))
                                {
                                    ConfirmText.text = "End Tutorial?";
                                }
                                break;
                            }
                        case FirstSelection.ExitGame:
                            {
                                ConfirmText.text = "Exit to Main Menu?";
                                break;
                            }
                    }
                    break;
                }
        }
    }

    private void Button1OnClick()
    {
        firstSelection = FirstSelection.Button1;
        menuState = MenuStates.ConfirmPanel;
    }

    private void ExitGameOnClick()
    {
        firstSelection = FirstSelection.ExitGame;
        menuState = MenuStates.ConfirmPanel;
    }
    
    private void ConfirmSelection(bool confirm)
    {
        if (confirm)
        {
            switch (firstSelection)
            {
                case FirstSelection.Button1:
                    {
                        if (SceneManager.GetActiveScene().name.Equals("TutorialScene"))
                        {
                            SceneManager.LoadScene("PaperboyScene");
                        }
                        break;
                    }
                case FirstSelection.ExitGame:
                    {
                        SceneManager.LoadScene("MainMenuScene");
                        break;
                    }
            }
        }
        else
        {
            menuState = MenuStates.MainPanel;
        }
    }

    public void UpdateScore(int delievered)
    {
        DelieveredAmount.text = delievered.ToString();
    }
}
