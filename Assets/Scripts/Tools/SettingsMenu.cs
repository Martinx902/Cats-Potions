using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField]
    private SO_Settings globalSettings;

    [SerializeField]
    private AudioMixer audioMixer;

    [SerializeField]
    private TMP_Dropdown resolutionsDropdown;

    [SerializeField]
    private TMP_Dropdown qualityDropdown;

    [SerializeField]
    private Slider volumeSlider;

    private Resolution[] resolutions;

    private void Start()
    {
        //We set the resolutions to our screen resolutions abelable

        resolutions = Screen.resolutions;

        resolutionsDropdown.ClearOptions();

        //Then we make them as a string list because the dropdowns don't allow resolutions by it self, so we got to make them strings chains

        List<string> optionList = new List<string>();

        int currentResolution = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            //We make them a string chain and add it to the list

            string newOption = resolutions[i].width + " x " + resolutions[i].height;

            optionList.Add(newOption);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                //If the resolution is our current resolution, we set it as it

                currentResolution = i;
            }
        }

        resolutionsDropdown.AddOptions(optionList);
        resolutionsDropdown.value = currentResolution;
        resolutionsDropdown.RefreshShownValue();

        SetSettings();
    }

    private void SetSettings()
    {
        if (globalSettings == null)
            Debug.LogError("No global settings added");

        SetVolume(globalSettings.volume);
        SetQuality(globalSettings.qualityLevel);
        SetFullScreen(globalSettings.isFullScreen);
        SetResolution(globalSettings.resolutionIndex);

        resolutionsDropdown.value = globalSettings.resolutionIndex;
        qualityDropdown.value = globalSettings.qualityLevel;
        volumeSlider.value = globalSettings.volume;
    }

    public void SetVolume(float inputVolume)
    {
        audioMixer.SetFloat("MainVolume", inputVolume);
        globalSettings.volume = inputVolume;
    }

    public void SetQuality(int quality)
    {
        QualitySettings.SetQualityLevel(quality);
        globalSettings.qualityLevel = quality;
    }

    public void SetFullScreen(bool isFullscreen)
    {
        globalSettings.isFullScreen = isFullscreen;
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        globalSettings.resolutionIndex = resolutionIndex;

        Resolution resolution = resolutions[resolutionIndex];

        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}