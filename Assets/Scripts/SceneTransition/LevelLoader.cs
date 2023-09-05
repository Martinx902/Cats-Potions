using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField]
    private Animator transition;

    [SerializeField]
    private float transitionTime;

    [SerializeField]
    private GameObject image;

    private void Awake()
    {
        image.SetActive(true);
    }

    public void LoadNextLevel(Scenes _sceneToGoTo)
    {
        string sceneToGoTo = "";

        switch (_sceneToGoTo)
        {
            case Scenes.Farm:
                sceneToGoTo = "MageSpot";
                break;

            case Scenes.Bear:
                sceneToGoTo = "BearSpot";
                break;

            case Scenes.Fox:
                sceneToGoTo = "FoxSpot";
                break;

            case Scenes.Lake:
                sceneToGoTo = "SpringSpot_2";
                break;

            case Scenes.Menu:
                sceneToGoTo = "StartMenu";
                break;

            case Scenes.None:
                Debug.Log("[Level Loader]: No scene to go to");
                break;

            case Scenes.MageHouse:
                sceneToGoTo = "MageInterior";
                break;

            case Scenes.HouseSecondFloor:
                sceneToGoTo = "MageInterior_2ndF";
                break;

            case Scenes.CursedLake:
                sceneToGoTo = "CursedSpringSpot";
                break;

            case Scenes.StartCinematic:
                sceneToGoTo = "StartCinematic";
                break;

            case Scenes.EndCinematic:
                sceneToGoTo = "EndCinematic";
                break;

            case Scenes.Credits:
                sceneToGoTo = "Credits";
                break;

            case Scenes.MenuCredits:
                sceneToGoTo = "MenuCredits";
                break;

            default:
                break;
        }

        PlayerPrefs.SetString("PreviousScene", SceneManager.GetActiveScene().name);

        StartCoroutine(LoadLevel(sceneToGoTo));
    }

    private IEnumerator LoadLevel(string sceneToGoTo)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(sceneToGoTo);
    }

    public void LoadMainMenu()
    {
        StartCoroutine(LoadLevel("StartMenu"));
    }

    private void SendPlayerToBed()
    {
        //Coroutine to send the player to bed after some time so we have time to play the falling asleep anim

        StartCoroutine(FallAsleep());
    }

    private IEnumerator FallAsleep()
    {
        yield return new WaitForSeconds(3.5f);

        LoadNextLevel(Scenes.HouseSecondFloor);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    #region Event Handlers

    private void OnEnable()
    {
        TimeController.pastHisBedtimeDelegate += SendPlayerToBed;
    }

    private void OnDisable()
    {
        TimeController.pastHisBedtimeDelegate -= SendPlayerToBed;
    }

    #endregion Event Handlers
}

public enum Scenes
{
    Farm,
    Bear,
    Fox,
    Lake,
    CursedLake,
    Menu,
    MageHouse,
    HouseSecondFloor,
    StartCinematic,
    EndCinematic,
    Credits,
    MenuCredits,
    None
}