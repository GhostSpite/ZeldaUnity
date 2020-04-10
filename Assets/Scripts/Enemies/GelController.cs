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

    public GameObject heartPrefab;
    public GameObject rupeePrefab;
    public GameObject bombPrefab;
    public GameObject clockPrefab;
    GameObject drop;
    
    public AudioClip die;

    AudioSource audioSource;
    Rigidbody2D rigidbody2d;

    // Start is called before the first frame update
    void Start()
    {
        rand = new System.Random(moveSeed);
        timer = moveTime;
        direction = 2;
        currentHealth = maxHealth;

        audioSource = GetComponent<AudioSource>();
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveWithAI();
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
            currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        }
        if (currentHealth <= 0)
        {
            PlaySound(die);
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

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
