﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LinkController : MonoBehaviour
{
    public static bool linkCanMove = true;
    bool triggerDead = true;

    TextScript text;
    CameraScript cam;

    public float projectileSpeed;
    public float movementSpeed;
    public float pushScale;
    public static bool boomerangPresent = false;

    public int life { get { return health.health; } }
    public int maxLife { get { return health.maxHealth; } }


    float arrowTimer;
    float swordTimer;
    float mouseTimer;
    public float invincibleTime;
    float invincibleTimer = 0f;
    bool invincible = false;

    public float lowLifeTime;
    bool counting = false;
    float healthTimer = 0f;
    public float mouseTime;
    bool clickTooFast;

    bool canShootSword;

    public GameObject swordShotPrefab;
    public GameObject arrowPrefab;
    public GameObject boomerangPrefab;
    public GameObject bombPrefab;

    public AudioClip swing;
    public AudioClip swordShotSound;
    public AudioClip bombPlace;
    public AudioClip arrowBoom;
    public AudioClip getHurt;
    public AudioClip die;
    public AudioClip lowLife;
    public bool pauseMusic;

    bool winning = false;

    AudioSource audioSource;

    Vector2 lookDirection = new Vector2(0f, -1f);

    Inventory inventory;
    Health health;
    Rigidbody2D rigidbody2d;
    Animator animator;
    Vector2 startPos;

    // ------------------ Core Methods ----------------
    void Start()
    {
        startPos = new Vector2(0, -4);
        text = GameObject.FindGameObjectWithTag("DeathText").GetComponent<TextScript>();
        cam = Camera.main.GetComponent<CameraScript>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        inventory = GetComponent<Inventory>();
        audioSource = GetComponent<AudioSource>();
        health = GetComponent<Health>();
        health.health = health.maxHealth;
        audioSource = GetComponent<AudioSource>();
        pauseMusic = false;
        canShootSword = true;
        clickTooFast = false;
        swordTimer = 0f;
    }

    void Update()
    {

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector2 posChange = new Vector2(horizontal, vertical);
        Vector2 position = rigidbody2d.position;

        if (!winning)
        {
            if (life <= 0)
            {
                if (triggerDead)
                {
                    linkCanMove = false;
                    DeathScene();
                }
            }
            else if (life <= 2)
            {
                if (!counting)
                {
                    PlaySound(lowLife);
                    healthTimer = lowLifeTime;
                    counting = true;
                }
                else
                {
                    healthTimer -= Time.deltaTime;

                    if (healthTimer < 0)
                    {
                        PlaySound(lowLife);
                        counting = false;
                    }
                }
            }
            else if (life < maxLife && canShootSword)
            {
                canShootSword = false;
            }
            else if (life == maxLife && !canShootSword)
            {
                canShootSword = true;
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

            if (linkCanMove)
            {
                posChange.Normalize();
                position += (posChange * movementSpeed * Time.deltaTime);
                rigidbody2d.position = position;
            }
            if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.N))
            {
                animator.SetTrigger("Attacking");
                PlaySound(swing);
                if (canShootSword && swordTimer <= 0)
                {
                    LaunchSword();
                    swordTimer = 1.5f;
                }
            }
            if (Input.GetMouseButton(0) && !clickTooFast)
            {
                animator.SetTrigger("Attacking");
                PlaySound(swing);
                if (canShootSword && swordTimer <= 0)
                {
                    LaunchSword();
                    swordTimer = 1.5f;
                }
                clickTooFast = true;
                mouseTimer = mouseTime;
            }

            if (arrowTimer > 0)
            {
                arrowTimer -= Time.deltaTime;
            }

            if (swordTimer > 0)
            {
                swordTimer -= Time.deltaTime;
            }

            if (mouseTimer > 0)
            {
                mouseTimer -= Time.deltaTime;
            }
            else
            {
                clickTooFast = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.B))
        {
            if (inventory.secActive == Inventory.Secondary.BOW && arrowTimer <= 0)
            {
                LaunchArrow();
            }

            if (inventory.secActive == Inventory.Secondary.RANG && !boomerangPresent)
            {
                LaunchBoomerang();
                boomerangPresent = true;
            }

            if (inventory.secActive == Inventory.Secondary.BOMB && inventory.bombs > 0)
            {
                PlaceBomb();
            }
        }

        if (Input.GetMouseButton(1) && !clickTooFast)
        {
            if (inventory.secActive == Inventory.Secondary.BOW && arrowTimer <= 0)
            {
                LaunchArrow();
            }

            if (inventory.secActive == Inventory.Secondary.RANG && !boomerangPresent)
            {
                LaunchBoomerang();
                boomerangPresent = true;
            }

            if (inventory.secActive == Inventory.Secondary.BOMB && inventory.bombs > 0)
            {
                PlaceBomb();
            }
            clickTooFast = true;
            mouseTimer = mouseTime;
        }
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            ChangeHealth(-1);
        }
        if (Input.GetKeyDown(KeyCode.C) && !triggerDead)
        {
            Reset();
        }
        if (Input.GetKeyDown(KeyCode.R) && !triggerDead)
        {
            triggerDead = true;
            linkCanMove = true;
            SceneManager.LoadScene("TitleScene");
        }

        if (invincible)
        {
            invincibleTimer -= Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.E))
            {
                ChangeHealth(-1);
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
                PlaySound(getHurt);
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

    public void ChangeTriforceCount(int amount)
	{
        inventory.triforce += amount;
        animator.SetTrigger("Triforce");
        linkCanMove = false;
        pauseMusic = true;
        winning = true;
    }

    // ------------------ Equipment Collection Methods ---------------------
    public void CollectBow()
    {
        inventory.hasBow = true;
        if (inventory.secActive == Inventory.Secondary.NONE)
        {
            inventory.secActive = Inventory.Secondary.BOW;
        }
    }

    public void CollectBoomerang()
    {
        inventory.hasRang = true;
        if (inventory.secActive == Inventory.Secondary.NONE)
        {
            inventory.secActive = Inventory.Secondary.RANG;
        }
    }

    public void CollectCompass()
    {
        inventory.hasCompass = true;
    }

    public void CollectMap()
    {
        inventory.hasMap = true;
        inventory.map.enabled = true;
    }

    public void CollectCandle()
    {
        inventory.hasCandle = true;
        if (inventory.secActive == Inventory.Secondary.NONE)
        {
            inventory.secActive = Inventory.Secondary.CANDLE;
        }
    }

    // --------------------- Attack Methods -------------------------
    void LaunchArrow()
    {
        if (inventory.rupees > 0 && inventory.hasBow && inventory.secActive == Inventory.Secondary.BOW)
        {
            GameObject arrow = Instantiate(arrowPrefab, rigidbody2d.position + Vector2.up * 0.2f, Quaternion.identity);

            ArrowProjectileController arrowProjectile = arrow.GetComponent<ArrowProjectileController>();
            arrowProjectile.Launch(lookDirection, projectileSpeed);
            inventory.rupees--;
            arrowTimer = 1.5f;
            PlaySound(arrowBoom);
        }   
    }

    void LaunchSword()
    {
        GameObject sword = Instantiate(swordShotPrefab, rigidbody2d.position + Vector2.up * 0.2f, Quaternion.identity);

        SwordShotController swordShot = sword.GetComponent<SwordShotController>();
        swordShot.Launch(lookDirection, projectileSpeed);
        swordTimer = 1.5f;
        PlaySound(swordShotSound);

    }

    void LaunchBoomerang()
    {
        if (inventory.hasRang && inventory.secActive == Inventory.Secondary.RANG)
        {
            GameObject boomerang = Instantiate(boomerangPrefab, rigidbody2d.position + Vector2.up * .2f, Quaternion.identity);
            BoomerangProjectileController boomerangProjectile = boomerang.GetComponent<BoomerangProjectileController>();
            boomerangProjectile.Launch(lookDirection, projectileSpeed);
            PlaySound(arrowBoom);
        }
    }

    void PlaceBomb()
    {
        if(inventory.bombs > 0 && inventory.secActive == Inventory.Secondary.BOMB)
        {
            GameObject bomb = Instantiate(bombPrefab, rigidbody2d.position, Quaternion.identity);
            inventory.bombs--;
            PlaySound(bombPlace);
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
            o.SetActive(false);
        }
        pauseMusic = true;
        StartCoroutine(grayOn());
        if (triggerDead) {
            animator.SetTrigger("Dead");
            PlaySound(die);
            triggerDead = false;
        }
        pauseMusic = true;
        StartCoroutine(WaitText());
        
        while (!Input.anyKey)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                Reset(); //reset at beginning w full health and all items
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("TitleScene");
            }
        }  
    }


    IEnumerator grayOn()
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
    IEnumerator grayOff()
    {
        foreach (GameObject g in Resources.FindObjectsOfTypeAll(typeof(GameObject)))
        {
            if (g.name.Contains("gray"))
            {
                g.SetActive(false);
                yield return new WaitForSeconds(1f);
            }
        }
        linkCanMove = true;
        pauseMusic = false;
        yield return new WaitForSeconds(1f);
        triggerDead = true;
    }

    public void Reset()
    {
        cam.GoBackToStart();
        Vector2 position = rigidbody2d.position;
        gameObject.transform.position = startPos;
        ChangeHealth(6);
        Debug.Log(life);
        foreach (GameObject g in Resources.FindObjectsOfTypeAll(typeof(GameObject)))
        {
            if (g.tag == "Enemy")
            {
                g.SetActive(true);
            }
        }
        text.DeleteText();
        StartCoroutine(grayOff());
    }

    IEnumerator WaitText()
    {
        yield return new WaitForSeconds(3f);
        text.PrintDeathText();
    }
}
