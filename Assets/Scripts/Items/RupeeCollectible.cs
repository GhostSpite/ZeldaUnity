using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RupeeCollectible : MonoBehaviour
{
    public bool collectible;
    public int amount;
    public AudioClip collected;

    private void OnTriggerEnter2D(Collider2D other)
    {
        LinkController controller = other.GetComponent<LinkController>();

        if (controller != null && collectible)
        {
            controller.ChangeRupeeCount(amount);

            Destroy(gameObject);

            controller.PlaySound(collected);
        }
    }
}
