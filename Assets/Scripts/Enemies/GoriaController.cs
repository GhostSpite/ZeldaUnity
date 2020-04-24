using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoriaController : MonoBehaviour
{
    public float speed;
    public float projectileSpeed;
    public int moveSeed;

    public float moveTime;
    float timer;
    public float attackTime;
    float attackTimer;
    public float freezeTime;
    float freezeTimer;

    bool frozen;
    public bool freeze { set { frozen = value; } get { return frozen; } }

    private int direction;
    private System.Random rand;
    private int lastDirection;
    private bool isStopped;
    private bool launched;

    public int maxHealth;
    public int health { get { return currentHealth; } }
    int currentHealth;

    public float invincibleTime;
    float invincibleTimer = 0f;
    bool invincible = false;

    DropItemUponDeath drop;
    Rigidbody2D rigidbody2d;
    public Animator animator;

    Vector2 lookDirection = new Vector2(0, 0);

    public GameObject projectilePrefab;
    
    void Start()
    {
        rand = new System.Random(moveSeed);
        timer = moveTime;
        attackTimer = attackTime;
        freezeTimer = freezeTime;
        direction = 2;
        lastDirection = direction;
        isStopped = false;
        launched = false;
        frozen = false;
        currentHealth = maxHealth;

        drop = GetComponent<DropItemUponDeath>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    
    void Update()
    {
        if (!frozen)
        {
            if (!isStopped)
            {
                moveWithAI();
            }
            else if (!launched)
            {
                Launch();
                launched = true;
            }
            else
            {
                attackTimer -= Time.deltaTime;
                if (attackTimer < 0)
                {
                    isStopped = false;
                    attackTimer = attackTime;
                    timer = 0;
                    launched = false;
                }
            }

            if (invincible)
            {
                invincibleTimer -= Time.deltaTime;

                if (invincibleTimer < 0)
                {
                    invincible = false;
                }
            }
        }
        else
        {
            freezeTimer -= Time.deltaTime;
            if (freezeTimer < 0)
            {
                frozen = false;
                freezeTimer = freezeTime;
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
                //dont move & thus attack
                isStopped = true;
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
        animator.SetFloat("Look Y", 1);
        animator.SetFloat("Look X", 0);
        lastDirection = direction;
        lookDirection.x = 0;
        lookDirection.y = 1;
        return position;
    }

    Vector2 moveDown(Vector2 position)
    {
        position.y -= speed * Time.deltaTime;
        animator.SetFloat("Look Y", -1);
        animator.SetFloat("Look X", 0);
        lastDirection = direction;
        lookDirection.x = 0;
        lookDirection.y = -1;
        return position;
    }

    Vector2 moveRight(Vector2 position)
    {
        position.x += speed * Time.deltaTime;
        animator.SetFloat("Look Y", 0);
        animator.SetFloat("Look X", 1);
        lastDirection = direction;
        lookDirection.x = 1;
        lookDirection.y = 0;
        return position;
    }

    Vector2 moveLeft(Vector2 position)
    {
        position.x -= speed * Time.deltaTime;
        animator.SetFloat("Look Y", 0);
        animator.SetFloat("Look X", -1);
        lastDirection = direction;
        lookDirection.x = -1;
        lookDirection.y = 0;
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

    void Launch()
    {
        GameObject projectile = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.2f, Quaternion.identity);

        BoomerangProjectileController boomerangProjectile = projectile.GetComponent<BoomerangProjectileController>();
        boomerangProjectile.Launch(lookDirection, projectileSpeed);
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (!invincible)
            {
                DamageGoria(amount);
            }
        }
    }

    public void DamageGoria(int amount)
    {
        //animator.SetTrigger("Damaged");
        invincibleTimer = invincibleTime;
        invincible = true;
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        if (currentHealth <= 0)
        {
            StartCoroutine(wait());
            drop.dropItem(false, (int)rigidbody2d.position.x, rigidbody2d.position);
        }
    }

    public IEnumerator wait()
    {
        animator.SetTrigger("Dead");
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
