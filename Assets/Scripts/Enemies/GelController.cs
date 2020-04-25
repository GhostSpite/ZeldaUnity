using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GelController : MonoBehaviour
{
    public float speed;
    public int moveSeed;

    public float moveTime;
    float timer;

    private int direction;
    private System.Random rand;

    public int maxHealth;
    public int health { get { return currentHealth; } }
    int currentHealth;
    bool isDead;

    DropItemUponDeath drop;
    Rigidbody2D rigidbody2d;

    public Animator animator;

    void Start()
    {
        rand = new System.Random(moveSeed);
        timer = moveTime;
        direction = 2;
        currentHealth = maxHealth;

        isDead = false;
        drop = GetComponent<DropItemUponDeath>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    
    void Update()
    {
        if (!isDead)
        {
            moveWithAI();
        }
    }

    public void moveWithAI()
    {
        changeDirection();
        Vector2 position = rigidbody2d.position;
        switch (direction)
        {
            case -2:
                position = moveUp(position);
                break;
            case -1:
                position = moveLeft(position);
                break;
            case 1:
                position = moveRight(position);
                break;
            case 2:
                position = moveDown(position);
                break;
            case 0:
                changeDirection();
                break;
        }
        rigidbody2d.position = position;
    }

    void changeDirection()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            direction = rand.Next(-3, 3);
            timer = moveTime;
        }
    }

    Vector2 moveUp(Vector2 position)
    {
        position.y += speed * Time.deltaTime;
        return position;
    }

    Vector2 moveDown(Vector2 position)
    {
        position.y -= speed * Time.deltaTime;
        return position;
    }

    Vector2 moveRight(Vector2 position)
    {
        position.x += speed * Time.deltaTime;
        return position;
    }

    Vector2 moveLeft(Vector2 position)
    {
        position.x -= speed * Time.deltaTime;
        return position;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        LinkController controller = other.gameObject.GetComponent<LinkController>();

        if (controller != null)
        {
            controller.ChangeHealth(-1);
        }
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        }
        if (currentHealth <= 0)
        {
            StartCoroutine(wait());
            drop.dropItem(false, (int)rigidbody2d.position.x, rigidbody2d.position);
        }
    }

    IEnumerator wait()
    {
        animator.SetTrigger("Dead");
        isDead = true;
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
