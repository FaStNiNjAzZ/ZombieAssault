using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsScript : MonoBehaviour
{
    public int screenMode; 
    // Screen Mode Settings
    // 0 = Exclusive Full Screen (Default),
    // 1 = Borderless Window,
    // 2 = Windowed

    public int screenResolution; 
    // Screen Resolution Settings
    // 0 = 3840x2160,
    // 1 = 2560x1440,
    // 2 = 1920x1080 (Default),
    // 3 = 1280x720
    public bool isFullscreen;
    // Full Screen Settings
    // 0 = Not Fullscreen
    // 1 = Is Fullscreen

    public void ResolutionControl()
    {
        switch (screenResolution)
        {
            case 0:
                Screen.SetResolution(3840, 2160, isFullscreen);
                break;
            case 1:
                Screen.SetResolution(2560, 1440, isFullscreen);
                break;
            case 2:
                Screen.SetResolution(1920, 1080, isFullscreen);
                break;
            case 3:
                Screen.SetResolution(1280, 720, isFullscreen);
                break;
            default:
                Screen.SetResolution(1920, 1080, true);
                break;
        }
    }

    public void ScreenModeControl()
    {
        switch (screenMode)
        {
            case 0:
                Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
                isFullscreen = true;
                break;
            case 1:
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                isFullscreen = false;
                break;
            case 2:
                Screen.fullScreenMode = FullScreenMode.Windowed;
                isFullscreen = false;
                break;
            default:
                Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
                Screen.SetResolution(1920, 1080, true);
                break;
        }
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadGame()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        screenMode = data.screenMode;
        screenResolution = data.screenResolution;
        isFullscreen = data.isFullScreen;
    }
}
