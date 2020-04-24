using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTrigger : MonoBehaviour
{
    public AudioClip secret;
    AudioSource audioSource;
    public GameObject door;
    DoorSpriteController controller;
    bool triggered;

    void Start()
    {
        controller = door.GetComponent<DoorSpriteController>();
        audioSource = GetComponent<AudioSource>();
        triggered = false;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name.Contains("Block") && !triggered)
        {
            controller.trigger = true;
            PlaySound(secret);
            triggered = true;
        }
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
