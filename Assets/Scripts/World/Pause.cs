using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject pauseCanvas;

    bool isPaused;
    public KeyCode pause;

    void Start()
    {
        isPaused = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(pause))
        {
            ChangePause();
        }
    }

    void ChangePause()
    {
        isPaused = !isPaused;
        if(isPaused)
        {
            pauseCanvas.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            pauseCanvas.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}
