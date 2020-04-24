using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triforce : MonoBehaviour
{
    public bool collectible;
    public AudioClip collected;
    public GameObject winScreen;

    private void OnTriggerEnter2D(Collider2D other)
    {
        LinkController controller = other.GetComponent<LinkController>();
        if (controller != null && collectible)
        {
            controller.ChangeTriforceCount(1);
            // End Game
            winScreen.GetComponent<WinScreenController>().won = true;
            Destroy(gameObject);

            controller.PlaySound(collected);
        }
    }
}
