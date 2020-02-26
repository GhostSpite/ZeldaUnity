using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericItemScript : MonoBehaviour
{
    public bool collectible;

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
            // Do something

            Destroy(gameObject);
        }
    }
}
