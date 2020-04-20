using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballProjectileController : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    BoxCollider2D boxCollider;

    float speed;
    Vector2 direction;
    
    void Awake()
    {
        rigidbody2d = gameObject.GetComponent<Rigidbody2D>();
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
    }
    
    void Update()
    {
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
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        LinkController controller = other.gameObject.GetComponent<LinkController>();

        if (controller != null)
        {
            controller.ChangeHealth(-1);
        }
        Destroy(gameObject);
    }
}
