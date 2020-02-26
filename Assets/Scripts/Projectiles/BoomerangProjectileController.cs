using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangProjectileController : MonoBehaviour
{
    public string collisionTag;
    Rigidbody2D rigidbody2d;
    float time = 1f;
    float timer;
    float speed;
    float rotation = 0f;

    //Animator animator;
    Vector2 direction;

    // Start is called before the first frame update
    void Awake()
    {
        rigidbody2d = gameObject.GetComponent<Rigidbody2D>();
        //animator = gameObject.GetComponent<Animator>();

        timer = 2 * time;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        rotation += 5;
        if (rotation > 360) rotation = rotation % 360;
        rigidbody2d.MoveRotation(rotation);

        if (timer > time) 
        {
            Vector2 position = rigidbody2d.position;
            position += direction * speed * Time.deltaTime;
            rigidbody2d.position = position;
        } 
        else if (timer > 0)
        {
            Vector2 position = rigidbody2d.position;
            position += -1 * direction * speed * Time.deltaTime;
            rigidbody2d.position = position;
        } 
        else
        {
            Destroy(gameObject);
        }
    }

    public void Launch(Vector2 direction, float speed)
    {
        this.direction = direction;
        this.speed = speed;

        //rigidbody2d.angularVelocity = 360f;

        //animator.SetFloat("Look X", direction.x);
        //animator.SetFloat("Look Y", direction.y);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(collisionTag))
        {
            // Apply damage to enemies on collision
            Destroy(gameObject);
        }
        else
        {
            timer = time / 2;
        }
    }
}
