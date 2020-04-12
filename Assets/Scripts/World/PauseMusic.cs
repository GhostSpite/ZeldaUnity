using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMusic : MonoBehaviour
{
    AudioSource audioSource;
    public GameObject Link;
    LinkController linkController;
    bool paused;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        linkController = Link.GetComponent<LinkController>();
        paused = false;
    }

    void Update()
    {
        if (linkController.pauseMusic)
        {
            Pause();
            paused = true;
        }
        else if(paused)
        {
            Play();
            paused = false;
        }
    }

    public void Pause()
    {
        audioSource.mute = true;
    }

    public void Play()
    {
        audioSource.mute = false;
    }
}
