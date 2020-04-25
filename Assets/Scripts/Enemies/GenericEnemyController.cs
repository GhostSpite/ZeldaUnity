using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericEnemyController : MonoBehaviour
{
    public float speed;

    public float moveTime;
    float timer;

    public bool vertical;
    bool changeDirection;
    float direction = 1f;

    Rigidbody2D rigidbody2d;
    Animator animator;
    void Start()
    {
        timer = moveTime;

        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            if (changeDirection)
            {
                direction *= -1;
            }
            vertical = !vertical;
            changeDirection = !changeDirection;
            timer = moveTime;
        }

        Vector2 position = rigidbody2d.position;
        
        if (vertical)
        {
            //position.y += direction * speed * Time.deltaTime;
            animator.SetFloat("Look X", 0);
            animator.SetFloat("Look Y", direction);
        }
        else
        {
            //position.x += direction * speed * Time.deltaTime;
            animator.SetFloat("Look X", direction);
            animator.SetFloat("Look Y", 0);
        }

        rigidbody2d.position = position;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        LinkController controller = other.gameObject.GetComponent<LinkController>();

        if (controller != null) 
        {
            controller.ChangeHealth(-1);
        }
    }
}
