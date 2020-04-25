﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedPotionCollectible : MonoBehaviour
{
    public bool collectible;

    private void OnTriggerEnter2D(Collider2D other)
    {
        LinkController controller = other.GetComponent<LinkController>();
        if (controller != null && collectible)
        {
            controller.ChangeRedPotionCount(1);

            Destroy(gameObject);
        }
    }
}
