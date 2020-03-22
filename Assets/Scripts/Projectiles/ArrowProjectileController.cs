using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowProjectileController : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    Animator animator;
    BoxCollider2D boxCollider;

    float timer = 1.5f;
    float speed;
    Vector2 direction;

    // Start is called before the first frame update
    void Awake()
    {
        rigidbody2d = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
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
            // Apply damage to enemies on collision
        }
        Destroy(gameObject);
    }
}
