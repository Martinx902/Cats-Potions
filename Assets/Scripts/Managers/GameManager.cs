//Martin Pérez Villabrille
//Cat & Potions
//Last Modification 27/04/2022
//Change: Day and night cycle

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public delegate void NextDayEvent();

    public static event NextDayEvent nextDayDelegate;

    [SerializeField]
    private SO_GlobalTime globalTime;

    [SerializeField]
    private SO_LocalTime localTime;

    [SerializeField]
    private bool sceneHasNextDay = false;

    private void Start()
    {
        //Checks if there are any day differences between the last day the player was there and the actual day we are supposed to be in

        if (localTime.localDay < globalTime.totalGameDays && sceneHasNextDay)
        {
            var daysPased = globalTime.totalGameDays - localTime.localDay;

            Debug.Log("Days passed without visiting " + SceneManager.GetActiveScene() + ":" + daysPased);

            for (int i = 0; i < daysPased; i++)
            {
                NextDay();
            }

            //Saves the last day the player was at that scene
            localTime.localDay = globalTime.totalGameDays;
        }
        else
        {
            //Just in case something goes south
            localTime.localDay = globalTime.totalGameDays;
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            //NextDay();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenu.isPaused = true;
        }
    }

    private void OnEnable()
    {
        TimeController.pastHisBedtimeDelegate += NextDay;
    }

    private void OnDisable()
    {
        TimeController.pastHisBedtimeDelegate -= NextDay;
    }

    private void NextDay()
    {
        if (!sceneHasNextDay)
            return;

        //Invoca todos los métodos suscritos a este método
        if (nextDayDelegate.GetInvocationList().Length > 0 && nextDayDelegate != null)
            nextDayDelegate.Invoke();
    }
}