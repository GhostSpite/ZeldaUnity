using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Items
{
    public class HealthCollectible : MonoBehaviour
    {

        public bool collectible;
        public int amount;
        public AudioClip collected;

        private void OnTriggerEnter2D(Collider2D other)
        {
            LinkController controller = other.GetComponent<LinkController>();
            if (controller != null && collectible)
            {
                if (controller.life < controller.maxLife)
                {
                    controller.ChangeHealth(amount);

                    Destroy(gameObject);

                    controller.PlaySound(collected);
                }
            }
        }
    }
}