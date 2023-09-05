using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject controlsPanel;

    public void Play()
    {
        SceneManager.LoadScene("ProgrammingScene");
    }

    public void Controls()
    {
        if(controlsPanel.activeInHierarchy)
        {
            controlsPanel.SetActive(false);
        }
        else
        {
            controlsPanel.SetActive(true);
        }
    }

    public void Exit()
    {
        Application.Quit();
    }

}
