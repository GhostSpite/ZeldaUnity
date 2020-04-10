using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AquamentusController : MonoBehaviour
{
    public float speed;
    public float projectileSpeed;
    public float buffer;

    public float moveTime;
    float timer;
    public float attackTime;
    float attackTimer;
    public float animateTime;
    float animateTimer;

    Vector2 initialPos = new Vector2();

    private int direction;
    private System.Random rand = new System.Random();

    public int maxHealth;
    public int health { get { return currentHealth; } }
    int currentHealth;

    public float invincibleTime;
    float invincibleTimer = 0f;
    bool invincible = false;

    public GameObject heartContainerPrefab;
    GameObject drop;

    Rigidbody2D rigidbody2d;
    Animator animator;

    Vector2 lookDirection = new Vector2(-1, 0);

    public GameObject projectilePrefab;

    bool launched;
    
    void Start()
    {
        timer = moveTime;
        attackTimer = attackTime;
        animateTimer = animateTime;
        direction = -1;
        currentHealth = maxHealth;

        launched = false;

        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        initialPos = rigidbody2d.position;
        animator.SetFloat("Look X", -1);
        animator.SetFloat("Look Y", 0);
    }
    
    void Update()
    {
        moveWithAI();

        attackTimer -= Time.deltaTime;
        if (attackTimer < 0)
        {
            Launch();
            attackTimer = attackTime;
        }
        if (launched)
        {
            animateTimer -= Time.deltaTime;
            if (animateTimer < 0)
            {
                animator.SetFloat("Look X", -1);
                animator.SetFloat("Look Y", 0);
                launched = false;
                animateTimer = animateTime;
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

    public void moveWithAI()
    {
        changeDirection();
        Vector2 position = rigidbody2d.position;
        switch (direction)
        {
            case -1:
                position = moveLeft(position);
                break;
            case 1:
                position = moveRight(position);
                break;
            default:
                timer = 0;
                break;
        }
        if(position.x > initialPos.x + buffer || position.x < initialPos.x - buffer)
        {
            direction = -1 * direction;
        }
        rigidbody2d.position = position;
    }

    void changeDirection()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            direction = rand.Next(-2, 2);
            timer = moveTime;
        }
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
            controller.ChangeHealth(-2);
        }
    }

    void Launch()
    {
        launched = true;
        animator.SetFloat("Look X", 0);
        animator.SetFloat("Look Y", 1);
        GameObject projectileUp = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.2f, Quaternion.identity);
        GameObject projectileStraight = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.2f, Quaternion.identity);
        GameObject projectileDown = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.2f, Quaternion.identity);

        BoomerangProjectileController boomerangProjectileUp = projectileUp.GetComponent<BoomerangProjectileController>();
        boomerangProjectileUp.Launch(new Vector2(lookDirection.x, lookDirection.y + 0.5f), projectileSpeed);
        BoomerangProjectileController boomerangProjectileStraight = projectileStraight.GetComponent<BoomerangProjectileController>();
        boomerangProjectileStraight.Launch(lookDirection, projectileSpeed);
        BoomerangProjectileController boomerangProjectileDown = projectileDown.GetComponent<BoomerangProjectileController>();
        boomerangProjectileDown.Launch(new Vector2(lookDirection.x, lookDirection.y - 0.5f), projectileSpeed);

    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (!invincible)
            {
                DamageAquamentus(amount);
            }
        }
        if (currentHealth <= 0)
        {
            drop = Instantiate(heartContainerPrefab, rigidbody2d.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void DamageAquamentus(int amount)
    {
        //animator.SetTrigger("Damaged");
        invincibleTimer = invincibleTime;
        invincible = true;
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
    }
}
