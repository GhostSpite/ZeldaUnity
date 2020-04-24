using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveDamage : MonoBehaviour
{
    public int power;

    void Start()
    {
    }
    
    void Update()
    {

    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        LinkController controller = other.gameObject.GetComponent<LinkController>();

        if (controller != null)
        {
            controller.ChangeHealth(( -1 * power));
        }
        Destroy(gameObject);

    }

}
