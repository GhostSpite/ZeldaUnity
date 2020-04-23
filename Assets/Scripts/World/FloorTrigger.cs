using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTrigger : MonoBehaviour
{
    public AudioClip secret;
    AudioSource audioSource;
    public GameObject door;
    DoorSpriteController controller;

    void Start()
    {
        controller = door.GetComponent<DoorSpriteController>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name.Contains("Block"))
        {
            controller.trigger = true;
            PlaySound(secret);
        }
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
