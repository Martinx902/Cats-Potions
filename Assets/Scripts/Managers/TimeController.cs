using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeController : MonoBehaviour
{
    #region Inspector Variables

    [Header("GLOBAL TIME")]
    [Space(15)]
    [SerializeField]
    private SO_GlobalTime globalTime;

    [Header("Light Settings")]
    [Space(15)]
    [SerializeField]
    private Light sunLight;

    [SerializeField]
    private float maxSunLightIntesinty;

    [SerializeField]
    private Light moonLight;

    [SerializeField]
    private float maxMoonLightIntensity;

    [Space(15)]
    [SerializeField]
    private AnimationCurve lightChangeCurve;

    [Header("Time Settings")]
    [Space(15)]
    [SerializeField]
    private float timeMultipliyer;

    [Header("Color Settings")]
    [Space(15)]
    [SerializeField]
    private Color dayAmbientLight;

    [SerializeField]
    private Color nightAmbientLight;

    [Header("UI")]
    [Space(15)]
    [SerializeField]
    private TextMeshProUGUI timeText;

    #endregion Inspector Variables

    #region Delegates

    public delegate void PastHisBedtime();

    public static event PastHisBedtime pastHisBedtimeDelegate;

    #endregion Delegates

    private bool showMessage = false;

    private bool advanceTime = true;

    private void Start()
    {
        globalTime.InstatiateGlobalTime();

        if (globalTime.lastTimeText != null)
        {
            timeText.text = globalTime.lastTimeText;
        }
    }

    private void Update()
    {
        if (!advanceTime)
            return;

        UpdateTimeOfDay();
        RotateSun();
        UpdateLightSettings();

        //Check to see if it's getting late and notify the player
        CheckTimeOfDay();
    }

    public void PauseTime(bool _advanceTime)
    {
        advanceTime = !_advanceTime;
    }

    private void CheckTimeOfDay()
    {
        if (globalTime.currentTime.Hour >= (globalTime.sleepTime.Hours - 2) && !showMessage)
        {
            PopUpUI.instance.Show(PopUpType.TimeToGoToBed);
            showMessage = true;
        }

        if (globalTime.currentTime.Hour >= globalTime.sleepTime.Hours)
        {
            //Throw some kind of event to notify someone that the player has overstayed
            advanceTime = false;

            globalTime.ResetDayTime();

            showMessage = false;

            pastHisBedtimeDelegate.Invoke();
        }
    }

    /// <summary>
    /// Sets the current time to the UI and updates it internally
    /// </summary>
    private void UpdateTimeOfDay()
    {
        if (timeText != null)
        {
            if ((globalTime.currentTime.Minute + 1) % 10 == 1)
            {
                timeText.text = globalTime.currentTime.ToString("HH:mm");
                globalTime.lastTimeText = timeText.text;
            }
        }

        globalTime.currentTime = globalTime.currentTime.AddSeconds(Time.deltaTime * timeMultipliyer);
    }

    /// <summary>
    /// Rotates the directional light depending on the sunset and sunrise positions
    /// </summary>
    private void RotateSun()
    {
        float sunLightRot;

        if (globalTime.currentTime.TimeOfDay > globalTime.sunriseTime && globalTime.currentTime.TimeOfDay < globalTime.sunsetTime)
        {
            //Day

            TimeSpan sunriseToSunsetDuration = CalculateTimeDifference(globalTime.sunriseTime, globalTime.sunsetTime);
            TimeSpan timeSinceSunrise = CalculateTimeDifference(globalTime.sunriseTime, globalTime.currentTime.TimeOfDay);

            double percentage = timeSinceSunrise.TotalMinutes / sunriseToSunsetDuration.TotalMinutes;

            sunLightRot = Mathf.Lerp(0, 180, (float)percentage);
        }
        else
        {
            //Night

            TimeSpan sunsetToSunriseDuration = CalculateTimeDifference(globalTime.sunsetTime, globalTime.sunriseTime);
            TimeSpan timeSinceSunset = CalculateTimeDifference(globalTime.sunsetTime, globalTime.currentTime.TimeOfDay);

            double percentage = timeSinceSunset.TotalMinutes / sunsetToSunriseDuration.TotalMinutes;

            sunLightRot = Mathf.Lerp(180, 360, (float)percentage);
        }

        sunLight.transform.rotation = Quaternion.AngleAxis(sunLightRot, Vector3.right);
    }

    /// <summary>
    /// Updates the intensity of the lights depeding on their rotation
    /// </summary>
    private void UpdateLightSettings()
    {
        float dotProduct = Vector3.Dot(sunLight.transform.forward, Vector3.down);

        sunLight.intensity = Mathf.Lerp(0, maxSunLightIntesinty, lightChangeCurve.Evaluate(dotProduct));

        moonLight.intensity = Mathf.Lerp(maxMoonLightIntensity, 0, lightChangeCurve.Evaluate(dotProduct));

        RenderSettings.ambientLight = Color.Lerp(nightAmbientLight, dayAmbientLight, lightChangeCurve.Evaluate(dotProduct));
    }

    private TimeSpan CalculateTimeDifference(TimeSpan fromTime, TimeSpan toTime)
    {
        TimeSpan difference = toTime - fromTime;

        if (difference.TotalSeconds < 0)
        {
            difference += TimeSpan.FromHours(24);
        }

        return difference;
    }
}