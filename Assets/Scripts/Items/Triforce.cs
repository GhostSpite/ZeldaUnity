using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triforce : MonoBehaviour
{
    public bool collectible;
    public AudioClip collected;
    public GameObject winScreen;
    public GameObject rest;

    private void OnTriggerEnter2D(Collider2D other)
    {
        LinkController controller = other.GetComponent<LinkController>();
        if (controller != null && collectible)
        {
            controller.ChangeTriforceCount(1);
            // End Game
            winScreen.GetComponent<WinScreenTextController>().go = true;
            Destroy(rest);
            Destroy(gameObject);

            controller.PlaySound(collected);
        }
    }
}
