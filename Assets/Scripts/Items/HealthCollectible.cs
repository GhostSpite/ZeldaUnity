using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Items
{
    public class HealthCollectible : MonoBehaviour
    {

        public bool collectible;
        public int amount;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            LinkController controller = other.GetComponent<LinkController>();
            if (controller != null && collectible)
            {
                if (controller.health < controller.maxHealth)
                {
                    controller.ChangeHealth(amount);

                    Destroy(gameObject);
                }
            }
        }
    }
}