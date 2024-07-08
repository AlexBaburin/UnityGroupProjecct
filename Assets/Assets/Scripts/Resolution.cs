using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Resolution : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown resolutionDropdown;

    private UnityEngine.Resolution[] resolutions;
    private List<UnityEngine.Resolution> filteredResolutions;

    private double currentRefreshRate;
    private int currentResolutionIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        resolutions = Screen.resolutions;
        filteredResolutions = new List<UnityEngine.Resolution>();

        resolutionDropdown.ClearOptions();
        currentRefreshRate = Screen.currentResolution.refreshRateRatio.value;

        for (int i = 0; i < resolutions.Length; i++)
        {
            if ((resolutions[i].refreshRateRatio.value == currentRefreshRate) && 
                (1.0f * resolutions[i].width / resolutions[i].height < (16f / 9f + 0.1f)
                && (1.0f * resolutions[i].width / resolutions[i].height > (16f / 9f - 0.1f))))
            {
                filteredResolutions.Add(resolutions[i]);
            }
        }

        List<string> options = new List<string>();
        for (int i = 0; i < filteredResolutions.Count; i++)
        {
            string resolutionOption = filteredResolutions[i].width + "x" + filteredResolutions[i].height + " " +
                filteredResolutions[i].refreshRateRatio.value + " Hz";
            options.Add(resolutionOption);
            if (filteredResolutions[i].width == Screen.width && filteredResolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }
    public void SetResolution(int resolutionIndex)
    {
        UnityEngine.Resolution resolution = filteredResolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, true);
    }
}
