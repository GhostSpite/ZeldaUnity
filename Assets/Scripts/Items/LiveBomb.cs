﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiveBomb : MonoBehaviour
{
    public float timer;
    public bool isLive;
    public GameObject explosion;
    public AudioClip explode;
    AudioSource audioSource;
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
    
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
                Rigidbody2D body = gameObject.AddComponent<Rigidbody2D>();
                body.gravityScale = 0;
            }
        } 
    }

    void DestroyBomb()
    {
        Destroy(gameObject);
    }

}
