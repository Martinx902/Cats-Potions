//Martin P�rez Villabrille
//Cat & Potions
//Last Modification 13/05/2023

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanelManager : MonoBehaviour
{
    private bool viewingPanel = false;

    private GameObject displayedPanel;

    private CharacterController player;

    private Transform pauseMenu;

    private GameObject mainPauseMenu;

    private TimeController timeController;

    private void Awake()
    {
        //Find the pause menu GO and its main child

        pauseMenu = GameObject.Find("PauseMenu").transform;

        mainPauseMenu = pauseMenu.Find("MainMenu").transform.gameObject;

        timeController = FindObjectOfType<TimeController>();

        player = FindObjectOfType<CharacterController>();
    }

    private void Update()
    {
        //Closes the panel with the ESC unless it's the Pause Menu
        if (displayedPanel != mainPauseMenu && viewingPanel && Input.GetKeyDown(KeyCode.Escape))
            DisplayPanel(displayedPanel);
    }

    /// <summary>
    /// Controls the activation of UI Panels
    /// </summary>
    /// <param name="newPanel"></param>
    public void DisplayPanel(GameObject newPanel)
    {
        //Check to see that panels do not overlap or can be activated various at the same time

        if (displayedPanel == newPanel)
        {
            ManagePanelActivation(newPanel);
        }
        else if (displayedPanel != newPanel)
        {
            if (displayedPanel == null || !displayedPanel.activeInHierarchy)
            {
                ManagePanelActivation(newPanel);
                displayedPanel = newPanel;
            }
        }
    }

    private void ManagePanelActivation(GameObject panelToActive)
    {
        if (panelToActive.activeInHierarchy)
        {
            viewingPanel = false;
            panelToActive.SetActive(false);
            player.CanMove(true);
            timeController.PauseTime(false);
        }
        else
        {
            viewingPanel = true;
            panelToActive.SetActive(true);
            player.CanMove(false);
            timeController.PauseTime(true);
        }
    }
}