using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiveBomb : MonoBehaviour
{
    public float timer;
    public bool isLive;
<<<<<<< HEAD
    public GameObject link;
    public GameObject explosion;
    public AudioClip explode;
    
=======
    AudioSource audioSource;
    Animator animator;

    // Start is called before the first frame update
>>>>>>> aba49fc46f44f7e94edc4a993f199a25e7601459
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
                isLive = false;
            }
        } 
    }

    void DestroyBomb()
    {
        Destroy(gameObject);
    }

}
