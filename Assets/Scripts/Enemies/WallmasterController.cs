using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallmasterController : MonoBehaviour
{
    AudioSource audioSource;
    Rigidbody2D rigidbody2d;
    Animator animator;

    Vector2 linkPosition;
    Vector2 startPosition;

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

    public GameObject heartPrefab;
    public GameObject rupeePrefab;
    public GameObject bombPrefab;
    public GameObject clockPrefab;
    GameObject drop;

    public AudioClip getHit;
    public AudioClip die;

    void Start()
    {
        rand = new System.Random(moveSeed);
        timer = moveTime;
        grabbed = false;
        startPosition = new Vector2(0, -4);
        currentHealth = maxHealth;

        audioSource = GetComponent<AudioSource>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       if (grabbed == true)
        {
            /*Vector2 position = rigidbody2d.position;
            moveDown(position);
            rigidbody2d.position = position;**/
            animator.SetFloat("Speed", speed);
        } else 
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
                    position = moveRight(position);
                    break;
            }
            rigidbody2d.position = position;

    }

    void changeDirection()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            direction = rand.Next(-1, 2);
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

        if (currentHealth <= 0)
        {
            PlaySound(die);
            dropItem();
            Destroy(gameObject);
        }
        else //if (!invincible)
        {
            PlaySound(getHit);
        }
    }

    public void DamageWallmaster(int amount)
    {
        //animator.SetTrigger("Damaged");
        invincibleTimer = invincibleTime;
        invincible = true;
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
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

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
