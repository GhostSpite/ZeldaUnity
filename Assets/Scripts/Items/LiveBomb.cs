using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiveBomb : MonoBehaviour
{
    public float timer;
    public bool isLive;
    AudioSource audioSource;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isLive)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                // Deal damage
                animator.SetTrigger("explode");
                audioSource.PlayOneShot(audioSource.clip);
                isLive = false;
            }
        } 
    }

    void DestroyBomb()
    {
        Destroy(gameObject);
    }

}
