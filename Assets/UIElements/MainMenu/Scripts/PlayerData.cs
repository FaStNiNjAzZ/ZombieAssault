using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerData
{
    // Player Stats
    public int playerTotalKills;
    public int playerTotalRounds;
    public int playerHighestRound;
    public int playerTotalDeaths;

    // Settings
    public int screenMode; // 0 = Exclusive Full Screen (Default), 1 = Borderless Window, 2 = Windowed
    public int screenResolution; // 0 = 3840x2160, 1 = 2560x1440, 2 = 1920x1080 (Default), 3 = 1280x720
    public bool isFullScreen;




    public PlayerData(SettingsScript settingsScript)
    {
        screenMode = settingsScript.screenMode;
        screenResolution = settingsScript.screenResolution;
        isFullScreen = settingsScript.isFullscreen;
    }
}