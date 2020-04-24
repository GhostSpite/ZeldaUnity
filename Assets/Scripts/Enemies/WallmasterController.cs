using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallmasterController : MonoBehaviour
{
    CameraScript cam;

    DropItemUponDeath drop;
    Rigidbody2D rigidbody2d;
    Animator animator;

    Vector2 linkPosition;
    Vector2 startPosition;

    public float freezeTime;
    float freezeTimer;

    bool frozen;
    public bool freeze { set { frozen = value; } get { return frozen; } }

    public float speed;
    public int moveSeed;

    private int direction;
    private System.Random rand;
    public float moveTime;
    float timer;
    private bool grabbed;

    public int maxHealth;
    public int health { get { return currentHealth; } }
    int currentHealth;

    public float invincibleTime;
    float invincibleTimer = 0f;
    bool invincible = false;

    void Start()
    {
        cam = Camera.main.GetComponent<CameraScript>();

        freezeTimer = freezeTime;
        rand = new System.Random(moveSeed);
        timer = moveTime;
        grabbed = false;
        frozen = false;
        startPosition = new Vector2(0, -4);
        currentHealth = maxHealth;

        drop = GetComponent<DropItemUponDeath>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    
    void Update()
    {
        if (!frozen)
        {
            if (grabbed == true)
            {
                /*Vector2 position = rigidbody2d.position;
                moveDown(position);
                rigidbody2d.position = position;**/
                //animator.SetFloat("Speed", speed);

            }
            else
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

    void moveWithAI()
    {

            changeDirection();
            Vector2 position = rigidbody2d.position;
            switch (direction)
            {
             case -1:
                    position = moveUp(position);
                    break;
             case 0:
                    position = moveRight(position);
                    break;
             case 1:
                    position = moveLeft(position);
                    break;
             case 2:
                    position = moveDown(position);
                    break;
            }
            rigidbody2d.position = position;

    }

    void changeDirection()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            direction = rand.Next(-1, 3);
            timer = rand.Next(1, 5);
        }

    }

    Vector2 moveUp(Vector2 position)
    {
        position.y += speed * Time.deltaTime;
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

    Vector2 moveDown(Vector2 position)
    {
        position.y -= speed * Time.deltaTime;
        return position;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        LinkController controller = other.gameObject.GetComponent<LinkController>();

        if (controller != null)
        {
            controller.ChangeHealth(-1);
            grabbed = true;
            Vector2 position = rigidbody2d.position;
            controller.transform.position = startPosition;
            cam.GoBackToStart();
        }
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (!invincible)
            {
                DamageWallmaster(amount);
            }
        }
    }

    public void DamageWallmaster(int amount)
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
