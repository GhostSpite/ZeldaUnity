using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowCollectible : MonoBehaviour
{
    public bool collectible;
    public AudioClip collected;

    private void OnTriggerEnter2D(Collider2D other)
    {
        LinkController controller = other.GetComponent<LinkController>();
        if (controller != null && collectible)
        {
            // Add item to inventory

            Destroy(gameObject);

            controller.PlaySound(collected);
            controller.CollectBow();
        }
    }
}
