using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    bool isPaused = false;

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeScreen();
            }
            else
            {
                PauseScreen();
            }
        }
        else if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            if (isPaused)
            {
                ResumeNoScreen();
            }
            else
            {
                PauseNoScreen();
            }
        }
    }

    void ResumeScreen()
    {
        //Pan away from pause screen
        ResumeNoScreen();
    }

    void PauseScreen()
    {
        //Pan to pause screen
        PauseNoScreen();
    }

    void ResumeNoScreen()
    {
        isPaused = false;
        Time.timeScale = 1f;
    }

    void PauseNoScreen()
    {
        isPaused = true;
        Time.timeScale = 0f;
    }
}
