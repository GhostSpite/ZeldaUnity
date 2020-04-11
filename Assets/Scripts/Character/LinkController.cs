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

    public int life { get { return health.health; } }
    public int maxLife { get { return health.maxHealth; } }

    public float invincibleTime;
    float invincibleTimer = 0f;
    bool invincible = false;

    public GameObject arrowPrefab;
    public GameObject boomerangPrefab;
    public GameObject bombPrefab;

    public AudioClip die;
    AudioSource audioSource;

    Vector2 lookDirection = new Vector2(0f, -1f);

    Inventory inventory;
    Health health;
    Rigidbody2D rigidbody2d;
    Animator animator;

    // ------------------ Core Methods ----------------
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        inventory = GetComponent<Inventory>();
        audioSource = GetComponent<AudioSource>();
        health = GetComponent<Health>();
        health.health = health.maxHealth;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector2 posChange = new Vector2(horizontal, vertical);
        Vector2 position = rigidbody2d.position;

        if (life <= 0)
        {
            DeathScene();
        }

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

    public void MoveLink(Vector2 posChange)
    {
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

    // ------------------- Health Methods ------------------
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
            health.health = Mathf.Clamp(health.health + amount, 0, health.maxHealth);
            
        }

        Debug.Log("Link: " +life + "/" + maxLife);
    }

    public void ChangeMaxHealth()
    {
        health.numberOfHearts = Mathf.Clamp(health.numberOfHearts + 1, 0, 10);
        health.health = Mathf.Clamp(health.health + 2, 0, health.numberOfHearts * 2);
    }

    void DamageLink(int amount){
        animator.SetTrigger("Damaged");
        invincibleTimer = invincibleTime;
        invincible = true;
        health.health = Mathf.Clamp(health.health + amount, 0, health.maxHealth);
    }

    // ------------------- Inventory Methods ------------------
    public void ChangeRupeeCount(int amount)
    {
        inventory.rupees += amount;
    }

    public void ChangeKeyCount(int amount)
    {
        inventory.keys += amount;
    }
    public void ChangeBombCount(int amount)
    {
        inventory.bombs += amount;
    }

    public void ChangeRedPotionCount(int amount)
    {
        inventory.redPotions += amount;
    }

    public void ChangeBluePotionCount(int amount)
    {
        inventory.bluePotions += amount;
    }

    public void RoomChange(Vector2 change)
    {
        Vector2 newPos = new Vector2(rigidbody2d.position.x + change.x, rigidbody2d.position.y + change.y);
        rigidbody2d.position = newPos;
    }

    // --------------------- Attack Methods -------------------------
    void LaunchArrow()
    {
        if (inventory.rupees > 0)
        {
            GameObject arrow = Instantiate(arrowPrefab, rigidbody2d.position + Vector2.up * 0.2f, Quaternion.identity);

            ArrowProjectileController arrowProjectile = arrow.GetComponent<ArrowProjectileController>();
            arrowProjectile.Launch(lookDirection, projectileSpeed);
            inventory.rupees--;
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
        if(inventory.bombs > 0)
        {
            GameObject bomb = Instantiate(bombPrefab, rigidbody2d.position, Quaternion.identity);
            inventory.bombs--;
        }
        
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    void DeathScene()
    {
        foreach (GameObject o in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Destroy(o);
        }

        StartCoroutine(wait());
        animator.SetTrigger("Dead");
        //play death sound
        PlaySound(die);
    }

    IEnumerator wait()
    {
        foreach (GameObject g in Resources.FindObjectsOfTypeAll(typeof(GameObject)))
        {
            if (g.name.Contains("gray"))
            {
                g.SetActive(true);
                yield return new WaitForSeconds(1f);
            }
        }
        
    }
}
