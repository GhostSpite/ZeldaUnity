using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericItemScript : MonoBehaviour
{
    public bool collectible;
    public AudioClip collected;

    private void OnTriggerEnter2D(Collider2D other)
    {
        LinkController controller = other.GetComponent<LinkController>();
        if (controller != null && collectible)
        {
            // Do something

            Destroy(gameObject);

            controller.PlaySound(collected);
        }
    }
}
