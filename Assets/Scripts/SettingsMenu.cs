using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Toggle fullscreenToggle;
    public Slider volumeSlider;

    public void Start()
    {
        if(Screen.fullScreen == true)
        {
            fullscreenToggle.isOn = true;
        }
        else
        {
            fullscreenToggle.isOn = false;
        }

        volumeSlider.value = PlayerPrefs.GetFloat("MasterVolume", 1f);
        gameObject.SetActive(false);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        if (Application.platform == RuntimePlatform.WebGLPlayer && !isFullscreen)
        {
            Screen.SetResolution(1280, 720, FullScreenMode.Windowed);
        }
        else if(isFullscreen)
        {
            Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, FullScreenMode.FullScreenWindow);
        }
        else if(!isFullscreen)
        {
            Screen.SetResolution(Screen.currentResolution.width/2, Screen.currentResolution.height/2, FullScreenMode.Windowed);
        }
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }
}
