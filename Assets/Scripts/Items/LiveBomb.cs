using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiveBomb : MonoBehaviour
{
    public float timer;
    public bool isLive;
    public GameObject link;
    public GameObject explosion;
    public AudioClip explode;
    
    void Start()
    {
        
    }
    
    void Update()
    {
        if (isLive)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                LinkController linkC = link.GetComponent<LinkController>();
                // Set explosion trigger for animator
                // Deal damage
                Destroy(gameObject);
                Instantiate(explosion, transform.position, Quaternion.identity);
                linkC.PlaySound(explode);

            }
        } 
    }

}
