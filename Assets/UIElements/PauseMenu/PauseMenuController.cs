using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject pauseMenuPanel;
    public GameObject otherPanel;

    void Start()
    {
        Resume();
    }

    public void MenuSelector(int selector)
    {
        switch (selector)
        {
            case 1:
                Pause();
                break;
            case 2:
                Resume();
                break;
            case 5:
                ExitGame();
                break;
            case 6:
                MainMenu();
                break;
        }
    }

    public void Pause()
    {
        Clear();
        Time.timeScale = 0;
        pauseMenuPanel.SetActive(true);
        mainPanel.SetActive(true);
        otherPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Resume()
    {
        Clear();
        Time.timeScale = 1;
        mainPanel.SetActive(false);
        otherPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;   // Making sure I can actually use the UI
        Cursor.visible = false;
    }

    void ExitGame()
    {
        Clear();
        Application.Quit();
    }

    // Load scene: 0
    void MainMenu()
    {
        Clear();
        SceneManager.LoadScene("MainMenuScene");
    }

    void Clear()
    {
        pauseMenuPanel.SetActive(false);
    }
}