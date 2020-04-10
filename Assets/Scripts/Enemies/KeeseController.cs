using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeeseController : MonoBehaviour
{
    public float speed;
    public float originalSpeed;
    public int moveSeed;

    private int direction;
    private System.Random rand;
    private bool isStopped;
    private bool startingUp;

    public float moveTime;
    public float stopTime;
    float timer;
    float stopTimer;
    public float deltaSpeed;

    public int maxHealth;
    public int health { get { return currentHealth; } }
    int currentHealth;

    public GameObject heartPrefab;
    public GameObject rupeePrefab;
    public GameObject bombPrefab;
    public GameObject clockPrefab;
    GameObject drop;

    Rigidbody2D rigidbody2d;
    Animator animator;
    
    void Start()
    {
        rand = new System.Random(moveSeed);
        timer = moveTime;
        stopTimer = stopTime;
        direction = 2;
        isStopped = false;
        currentHealth = maxHealth;

        startingUp = true;
        originalSpeed = speed;
        speed = 0;
        
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        
    }
    
    void Update()
    {
        moveWithAI();
    }

    void moveWithAI()
    {
        changeDirection();
        Vector2 position = rigidbody2d.position;
        //just walking around, stop when attacking
        switch (direction)
        {
            case -4:
                position = moveNW(position);
                break;
            case -3:
                position = moveSW(position);
                break;
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
            case 3:
                position = moveNE(position);
                break;
            case 4:
                position = moveSE(position);
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
    
        if (timer <= 0)
        {
            direction = rand.Next(-5, 5);
            timer = moveTime;
        }

        if (isStopped)
        {
            if(direction == 0)
            {
                direction = rand.Next(-5, 5);
            }
            speed -= deltaSpeed;
            animator.SetFloat("Speed" , speed);

            if (speed<=0)
            {
                speed = 0;
                animator.SetFloat("Speed", 0);
             
                stopTimer -= Time.deltaTime;

                if(stopTimer < 0)
                {
                    isStopped = false;
                    startingUp = true;
                    direction = rand.Next(-5, 5);
                    stopTimer = stopTime;
                }
            }
        }
        else if (startingUp)
        {
            speed += deltaSpeed;
            animator.SetFloat("Speed", speed);
            if(speed >= originalSpeed)
            {
                startingUp = false;
            }
        }
    }

    Vector2 moveUp(Vector2 position)
    {
        position.y += speed * Time.deltaTime;
        return position;
    }

    Vector2 moveNE(Vector2 position)
    {
        position.y += speed * Time.deltaTime;
        position.x += speed * Time.deltaTime;
        return position;
    }

    Vector2 moveRight(Vector2 position)
    {
        position.x += speed * Time.deltaTime;
        return position;
    }

    Vector2 moveSE(Vector2 position)
    {
        position.x += speed * Time.deltaTime;
        position.y -= speed * Time.deltaTime;
        return position;
    }

    Vector2 moveDown(Vector2 position)
    {
        position.y -= speed * Time.deltaTime;
        return position;
    }

   Vector2 moveSW(Vector2 position)
    {
        position.y -= speed * Time.deltaTime;
        position.x -= speed * Time.deltaTime;
        return position;
    }

    Vector2 moveLeft(Vector2 position)
    {
        position.x -= speed * Time.deltaTime;
        return position;
    }

     Vector2 moveNW(Vector2 position)
    {
        position.x -= speed * Time.deltaTime;
        position.y += speed * Time.deltaTime;
        return position;
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        LinkController controller = other.gameObject.GetComponent<LinkController>();

        if (controller != null)
        {
            controller.ChangeHealth(-1);
        }
        else
        {
            direction = rand.Next(-5, 5);
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
            dropItem();
            Destroy(gameObject);
        }
    }

    void dropItem()
    {
        int item = rand.Next(0, 20);
        switch (item)
        {
            case 0:
                drop = Instantiate(rupeePrefab, rigidbody2d.position, Quaternion.identity);
                break;
            case 1:
                drop = Instantiate(rupeePrefab, rigidbody2d.position, Quaternion.identity);
                break;
            case 2:
                drop = Instantiate(heartPrefab, rigidbody2d.position, Quaternion.identity);
                break;
            case 3:
                drop = Instantiate(heartPrefab, rigidbody2d.position, Quaternion.identity);
                break;
            case 4:
                drop = Instantiate(bombPrefab, rigidbody2d.position, Quaternion.identity);
                break;
            case 5:
                drop = Instantiate(bombPrefab, rigidbody2d.position, Quaternion.identity);
                break;
            case 6:
                drop = Instantiate(clockPrefab, rigidbody2d.position, Quaternion.identity);
                break;
            default:
                break;
        }
    }
}
