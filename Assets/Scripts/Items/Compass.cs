using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass : MonoBehaviour
{

    public GameObject triforceIcon;
    public GameObject linkIcon;

    public bool collectible;
    public AudioClip collected;

    private void OnTriggerEnter2D(Collider2D other)
    {
        LinkController controller = other.GetComponent<LinkController>();
        if (controller != null && collectible)
        {
            triforceIcon.SetActive(true);
            linkIcon.SetActive(true);

            controller.CollectCompass();

            Destroy(gameObject);

            controller.PlaySound(collected);
        }
    }
}
