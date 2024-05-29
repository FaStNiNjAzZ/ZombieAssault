using System;
using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public GameObject menuManager;

    public GameObject MainMenuCanvas;

    //Menu Panels
    public GameObject mainMenuPanel;
    public GameObject multiplayerMenuPanel;
    public GameObject lobbyMenuPanel;
    public GameObject optionsMenuPanel;
    public GameObject resolutionOptionsMenuPanel;
    public GameObject windowOptionsMenuPanel;
    public GameObject barracksMultiplayerMenuPanel;
    public GameObject progressionMultiplayerMenuPanel;
    public GameObject mapSelectionPanel;
    public GameObject statsMenuPanel;

    // Map Info Panels
    public GameObject arenaMapInfoPanel;
    public GameObject shipwreckedMapInfoPanel;
    public GameObject placeHolderMapInfoPanel;

    // Misc Panels
    public GameObject debugMainPanel;

    
    public Text levelText;
    public Text callingCardPrestigeAndLevelText;

    int menuLevel;
    int menuPrestige;

    // Start is called before the first frame update
    void Start()
    {
        // Initial Start up, loads the variables and automatically run through the settings them.
        SettingsScript settingsScript = GetComponent<SettingsScript>(); 
        settingsScript.LoadGame();
        settingsScript.ResolutionControl();
        settingsScript.ScreenModeControl();
    }

    // Update is called once per frame
    void Update()
    {
        // Leveling Stuff (Don't worry about using it)
        LevelingScriptVariableUpdater();
        LevelUpdater();
        CallingCardStatsInfoUpdater();
    }

    void LevelUpdater()
    {
        if (menuLevel > 0)
        {
            levelText.text = menuLevel.ToString();
        }  
    }

    void CallingCardStatsInfoUpdater()
    {
        if (menuLevel > 0)
        {
            if (menuPrestige < 1)
            {
                callingCardPrestigeAndLevelText.text = ("Level " + menuLevel.ToString());
            }
            else
            {
                callingCardPrestigeAndLevelText.text = ("Prestige " + menuPrestige.ToString() + " " + "Level " + menuLevel.ToString());
            } 
        }
    }

    void LevelingScriptVariableUpdater()
    {
        LevelingScript levelingScript = GetComponent<LevelingScript>();
        menuLevel = levelingScript.menLevel;
        menuPrestige = levelingScript.menPrestige;
    }

    void ExitAllPanels()
    {
        mainMenuPanel.SetActive(false);
        multiplayerMenuPanel.SetActive(false);
        lobbyMenuPanel.SetActive(false);
        optionsMenuPanel.SetActive(false);
        resolutionOptionsMenuPanel.SetActive(false);
        windowOptionsMenuPanel.SetActive(false); 
        barracksMultiplayerMenuPanel.SetActive(false);
        progressionMultiplayerMenuPanel.SetActive(false);
        mapSelectionPanel.SetActive(false);
        statsMenuPanel.SetActive(false);

        arenaMapInfoPanel.SetActive(false);
        shipwreckedMapInfoPanel.SetActive(false);
        placeHolderMapInfoPanel.SetActive(false);
    }

    // Menu Buttons

    public void MainMenuButton()
    {
        ExitAllPanels();
        mainMenuPanel.SetActive(true);
    }

    //Main Menu Buttons
    public void MapSelectionButton()
    {
        ExitAllPanels();
        mapSelectionPanel.SetActive(true);
    }

    public void MultiplayerButton()
    {
        ExitAllPanels();
        multiplayerMenuPanel.SetActive(true);
        lobbyMenuPanel.SetActive(true);
    }

    public void StatsButton()
    {
        ExitAllPanels();
        statsMenuPanel.SetActive(true);
    }

    public void OptionsButton()
    {
        ExitAllPanels();
        optionsMenuPanel.SetActive(true);
    }

    // Map Info Panels, Hover On & Off
    public void MapInfoButtonHoverOff()
    {
        arenaMapInfoPanel.SetActive(false);
        shipwreckedMapInfoPanel.SetActive(false);
        placeHolderMapInfoPanel.SetActive(false);
    }

    public void MapInfoButtonHoverOn(int mapIDs)
    {
        //Map IDs:
        // 0 = !Placeholder!
        // 1 = Arena of the Undead,
        // 2 = Shipwrecked Souls,
        switch (mapIDs)
        {
            case 0:
                placeHolderMapInfoPanel.SetActive(true);
                break;
            case 1:
                arenaMapInfoPanel.SetActive(true);
                break;
            case 2:
                shipwreckedMapInfoPanel.SetActive(true);
                break;
        }
    }
    // Map Load Button
    public void LoadArena(int mapID)
    {
        //Map IDs:
        // 0 = Test Zone,
        // 1 = Arena of the Undead,
        // 2 = Shipwrecked Souls,
        Debug.Log("load map " + mapID);
        MainMenuCanvas.SetActive(false);
        ExitAllPanels();
        LoadingScreen loadingScreen = menuManager.GetComponent<LoadingScreen>();
        loadingScreen.LoadScene(mapID);
    }

    

    //Options Menu Buttons
    public void ResolutionButton()
    {
        ExitAllPanels();
        resolutionOptionsMenuPanel.SetActive(true);
    }

    public void WindowedButton()
    {
        ExitAllPanels();
        windowOptionsMenuPanel.SetActive(true);
    }

    //Resolution Menu Buttons
    public void ChangeResolutionButton(int Selection)
    {
        SettingsScript settingsScript = GetComponent<SettingsScript>();

        settingsScript.screenResolution = Selection;
        settingsScript.ResolutionControl();
        settingsScript.SavePlayer();
    }

    public void ChangeScreenButton(int Selection)
    {
        SettingsScript settingsScript = GetComponent<SettingsScript>();

        settingsScript.screenMode = Selection;
        settingsScript.ScreenModeControl();
        settingsScript.SavePlayer();
    }

    //Multiplayer Menu Buttons (Don't worry about using it)
    public void BarracksButton()
    {
        ExitAllPanels();
        barracksMultiplayerMenuPanel.SetActive(true);
    }

    public void ProgressionButton()
    {
        ExitAllPanels();
        progressionMultiplayerMenuPanel.SetActive(true);
    }

    // Debug (Don't worry about using it)
    public void DebugPanelExit()
    {
        debugMainPanel.SetActive(false);
    }

    public void DebugPanelOpen()
    {
        debugMainPanel.SetActive(true);
    }

    // Misc
    public void ExitGame()
    {
        Application.Quit();
    }
}
