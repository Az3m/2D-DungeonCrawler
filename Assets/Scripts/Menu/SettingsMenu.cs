using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    Resolution[] resolutions;
    public TMPro.TMP_Dropdown resolutionDropdown;


    private void Start()
    { // Toata chestia asta populeaza dropdown-ul cu rezolutiile pe care le putem folosii si seteaza rezolutia default ca cea folosita deja
        resolutions = Screen.resolutions;
        int currResolutionIndex = 0;

        resolutionDropdown.ClearOptions();

        List<string> dropdownOptions = new List<string>();

        for(int i = 0;  i < resolutions.Length; i++) 
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;

            dropdownOptions.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currResolutionIndex = i;
            }

        }

        resolutionDropdown.AddOptions(dropdownOptions);
        resolutionDropdown.value = currResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResoultion(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
