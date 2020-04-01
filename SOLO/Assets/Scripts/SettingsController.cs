using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingsController : MonoBehaviour
{
    public AudioMixer audioMixer;
    public TMP_Dropdown resolutionDropDown;
    public GameObject container;

    Resolution[] resolutions;
    Vector3 baseScale;
    Vector3 scaleToShrink;

    private void Start()
    {
        baseScale = container.transform.localScale;
        HideContainer();

        resolutions = Screen.resolutions;

        resolutionDropDown.ClearOptions();

        int currentIndex = 0;

        List<string> options = new List<string>();
        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentIndex = i;
            }

            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);
        }
        resolutionDropDown.AddOptions(options);
        resolutionDropDown.value = currentIndex;
        resolutionDropDown.RefreshShownValue();
    }

    private void Update()
    {
        if (transform.localScale != scaleToShrink)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, scaleToShrink, 0.5f);
        }
    }

    public void ChangeVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }

    public void SetQuality(int index)
    {
        Debug.Log(index);
        QualitySettings.SetQualityLevel(index);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int index)
    {
        Screen.SetResolution(resolutions[index].width, resolutions[index].height, Screen.fullScreen);
    }

    public void ShowContainer()
    {
        scaleToShrink = baseScale;
    }

    public void HideContainer()
    {
        scaleToShrink = new Vector3(0, 0, 0);
    }
}
