using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiveBomb : MonoBehaviour
{
    public float timer;
    public bool isLive;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isLive)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                // Set explosion trigger for animator
                // Deal damage
                Destroy(gameObject);
            }
        }
    }
}
