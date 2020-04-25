using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowProjectileController : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    Animator animator;
    BoxCollider2D boxCollider;
    AudioSource audioSource;

    public AudioClip fly;

    float timer = 1.5f;
    float speed;
    Vector2 direction;
    
    void Awake()
    {
        rigidbody2d = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        audioSource = gameObject.GetComponent<AudioSource>();

        PlaySound(fly);
    }
    
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0) Destroy(gameObject);

        Vector2 position = rigidbody2d.position;
        position += direction * speed * Time.deltaTime;
        rigidbody2d.position = position;
    }

    public void Launch(Vector2 direction, float speed)
    {
        this.direction = direction;
        this.speed = speed;

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            Vector2 size = boxCollider.size;
            size.y = size.x;
            size.x = 1;
            boxCollider.size = size;
        }
        animator.SetFloat("Look X", direction.x);
        animator.SetFloat("Look Y", direction.y);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            GetComponent<ProjectileDamage>().damageEnemy(other.collider);
        }
        Destroy(gameObject);
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
