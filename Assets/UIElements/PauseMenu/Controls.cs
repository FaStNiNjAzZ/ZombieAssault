using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    bool alreadyPressed = false;
    bool isPaused = false;

    public PauseMenuController pauseMenuController;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenuController = GameObject.Find("UIController").GetComponent<PauseMenuController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!alreadyPressed)
            {
                if (!isPaused)
                {
                    isPaused = true;
                    pauseMenuController.Pause();
                }

                else if (isPaused)
                {
                    isPaused = false;
                    pauseMenuController.Resume();
                }
            }
            alreadyPressed = true;
        }
        if (Input.GetKeyUp(KeyCode.Escape)) { alreadyPressed = false; }
    }
}
