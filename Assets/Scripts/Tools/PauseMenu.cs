using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;

    [SerializeField]
    private GameObject pauseMenuUI, controlsMenuUI, settingsMenuUI;

    //Time normal, hide pause panel, game is not paused
    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    //Show the Pause Panel, stop time, say the game is paused
    private void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ChangePanels(int panelIndex)
    {
        //Could also be done with an enum and giving more use to the static variable, but I think thats an overkill for this
        //project so I'll keep it functional and simple just for now. 18/05/2022

        switch (panelIndex)
        {
            //Main Pause Panel
            case 0:
                controlsMenuUI.SetActive(false);
                settingsMenuUI.SetActive(false);
                break;
            //Controls Panel
            case 1:
                controlsMenuUI.SetActive(true);
                settingsMenuUI.SetActive(false);
                break;
            //Settings Panel
            case 2:
                controlsMenuUI.SetActive(false);
                settingsMenuUI.SetActive(true);
                break;

            default:
                Debug.Log("Problemas con el selector de menus, en el switch");
                break;
        }
    }

    public void ExitGame()
    {
        SceneManager.LoadScene("StartMenu");
    }
}