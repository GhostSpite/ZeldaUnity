using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalfosController : MonoBehaviour
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

    public float invincibleTime;
    float invincibleTimer = 0f;
    bool invincible = false;

    public bool hasKey;

    DropItemUponDeath drop;
    Rigidbody2D rigidbody2d;
    Animator animator;
    
    void Start()
    {
        rand = new System.Random(moveSeed);
        timer = moveTime;
        direction = 2;
        currentHealth = maxHealth;

        drop = GetComponent<DropItemUponDeath>();
        rigidbody2d = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        moveWithAI();

        if (invincible)
        {
            invincibleTimer -= Time.deltaTime;

            if (invincibleTimer < 0)
            {
                invincible = false;
            }
        }
    }

    public void moveWithAI()
    {
        changeDirection();
        Vector2 position = rigidbody2d.position;
        //just walking around, stop when attacking
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
            if (!invincible)
            {
                DamageStalfos(amount);
            }
        }

        if (currentHealth <= 0)
        {
            drop.dropItem(hasKey, moveSeed, rigidbody2d.position);
            Destroy(gameObject);
        }
    }

    public void DamageStalfos(int amount)
    {
        //animator.SetTrigger("Damaged");
        invincibleTimer = invincibleTime;
        invincible = true;
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
    }
}
