using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LinkController : MonoBehaviour
{
    public float projectileSpeed;
    public float movementSpeed;
    public float pushScale;
    public static bool boomerangPresent = false;

    public int health { get { return currentHealth; } }
    public int maxHealth;
    int currentHealth;

    public float invincibleTime;
    float invincibleTimer = 0f;
    bool invincible = false;

    int bombCounter = 0;
    int redPotionCounter = 0;
    int bluePotionCounter = 0;
    int rupeeCounter = 0;

    public GameObject arrowPrefab;
    public GameObject boomerangPrefab;
    public GameObject bombPrefab;

    Vector2 lookDirection = new Vector2(0f, -1f);

    Rigidbody2D rigidbody2d;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector2 posChange = new Vector2(horizontal, vertical);
        Vector2 position = rigidbody2d.position;

        if (boomerangPresent)
        {
            posChange = new Vector2(0, 0);
        }
        if (posChange != Vector2.zero)
        {
            MoveLink(posChange);
        }
        else
        {
            animator.SetBool("Moving", false);
        }

        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);

        posChange.Normalize();
        position += (posChange * movementSpeed * Time.deltaTime);

        rigidbody2d.position = position;

        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.N)){
            animator.SetTrigger("Attacking");
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)){
            LaunchArrow();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)){
            if (!boomerangPresent)
            {
                LaunchBoomerang();
            }
            boomerangPresent = true;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3)){
            PlaceBomb();
        }

        if (Input.GetKeyDown(KeyCode.E)){
            ChangeHealth(-1);
        }

        if (invincible){
            invincibleTimer -= Time.deltaTime;

            if(invincibleTimer < 0)
            {
                invincible = false;
            }
        }
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (!invincible)
            {
                DamageLink(amount);
            }
        }
        else
        {
            currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        }

        Debug.Log(currentHealth + "/" + maxHealth);
    }

    public void MoveLink(Vector2 posChange){
        if (!boomerangPresent)
        {
            animator.SetBool("Moving", true);
            animator.SetFloat("Move X", posChange.x);
            animator.SetFloat("Move Y", posChange.y);
            lookDirection = posChange;
        } 
        else
        {
            animator.SetBool("Moving", false);
        }
    }

    void DamageLink(int amount){
        animator.SetTrigger("Damaged");
        invincibleTimer = invincibleTime;
        invincible = true;
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
    }

    public void ChangeBombCount(int amount)
    {
        bombCounter += amount;
        Debug.Log("Link now has " + bombCounter + " bomb(s)");
    }

    public void ChangeRedPotionCount(int amount)
    {
        redPotionCounter += amount;
        Debug.Log("Link now has " + redPotionCounter + " red potion(s)");
    }

    public void ChangeBluePotionCount(int amount)
    {
        bluePotionCounter += amount;
        Debug.Log("Link now has " + bluePotionCounter + " blue potion(s)");
    }

    public void ChangeRupeeCount(int amount)
    {
        rupeeCounter += amount;
        Debug.Log("Link now has " + rupeeCounter + " rupee(s)");
    }

    public void RoomChange(Vector2 change)
    {
        Vector2 newPos = new Vector2(rigidbody2d.position.x + change.x, rigidbody2d.position.y + change.y);
        rigidbody2d.position = newPos;
    }

    void LaunchArrow()
    {
        if (rupeeCounter > 0)
        {
            GameObject arrow = Instantiate(arrowPrefab, rigidbody2d.position + Vector2.up * 0.2f, Quaternion.identity);

            ArrowProjectileController arrowProjectile = arrow.GetComponent<ArrowProjectileController>();
            arrowProjectile.Launch(lookDirection, projectileSpeed);
            rupeeCounter--;
        }
        
    }

    void LaunchBoomerang()
    {
        GameObject boomerang = Instantiate(boomerangPrefab, rigidbody2d.position + Vector2.up * .2f, Quaternion.identity);

        BoomerangProjectileController boomerangProjectile = boomerang.GetComponent<BoomerangProjectileController>();
        boomerangProjectile.Launch(lookDirection, projectileSpeed);
    }

    void PlaceBomb()
    {
        GameObject bomb = Instantiate(bombPrefab, rigidbody2d.position, Quaternion.identity);
    }
}
