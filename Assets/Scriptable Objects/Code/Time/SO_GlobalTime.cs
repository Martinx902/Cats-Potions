using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Time/Global Time")]
public class SO_GlobalTime : ScriptableObject
{
    [SerializeField]
    private float sunriseHour;

    [SerializeField]
    private float sunsetHour;

    [SerializeField]
    private float startHour;

    [SerializeField]
    private float sleepHour;

    [HideInInspector]
    public DateTime currentTime;

    [HideInInspector]
    public TimeSpan sunriseTime;

    [HideInInspector]
    public TimeSpan sunsetTime;

    [HideInInspector]
    public TimeSpan sleepTime;

    [SerializeField]
    private bool instantiated = false;

    public int totalGameDays = 0;

    [HideInInspector]
    public string lastTimeText;

    public void ResetDayTime()
    {
        totalGameDays++;
        instantiated = false;
    }

    public void InstatiateGlobalTime()
    {
        if (!instantiated)
        {
            currentTime = DateTime.Now.Date + TimeSpan.FromHours(startHour);

            sunriseTime = TimeSpan.FromHours(sunriseHour);

            sunsetTime = TimeSpan.FromHours(sunsetHour);

            sleepTime = TimeSpan.FromHours(sleepHour);

            instantiated = true;
        }
    }
}