using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSpriteController : MonoBehaviour
{
    public string direction;
    public string state;
    public bool bombable;
    bool isTriggered;
    public bool trigger { set { isTriggered = value; } }
    public bool openForEnemyDeath;
    public bool hasPartner;
    public GameObject partner;
    DoorSpriteController partnerController;
    bool solid;
    public List<GameObject> enemies;
    int numEnemies;

    public AudioClip open;

    AudioSource audioSource;
    Animator animator;
    Collider2D collider2d;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        collider2d = GetComponent<Collider2D>();
        if (hasPartner)
        {
            partnerController = partner.GetComponent<DoorSpriteController>();
        }

        numEnemies = enemies.Count;

        setState(state);
    }
    
    void Update()
    {
        if (state == "shut")
        {
            if (openForEnemyDeath)
            {
                for (int i = 0; i < numEnemies; i++)
                {
                    if (enemies[i] == null)
                    {
                        numEnemies--;
                        enemies.RemoveAt(i);
                    }
                }
                if (numEnemies == 0)
                {
                    PlaySound(open);
                    setState("door");
                    if (hasPartner)
                    {
                        partnerController.setState("door");
                    }
                }
            }
            else if (isTriggered)
            {
                PlaySound(open);
                setState("door");
                if (hasPartner)
                {
                    partnerController.setState("door");
                }
            }
        }
    }

    void setDirection(string dir)
    {
        switch (dir)
        {
            case "up":
                animator.SetFloat("Look X", 0);
                animator.SetFloat("Look Y", 1);
                break;
            case "down":
                animator.SetFloat("Look X", 0);
                animator.SetFloat("Look Y", -1);
                break;
            case "left":
                animator.SetFloat("Look X", -1);
                animator.SetFloat("Look Y", 0);
                break;
            case "right":
                animator.SetFloat("Look X", 1);
                animator.SetFloat("Look Y", 0);
                break;
        }
    }

    void setState(string s)
    {
        setDirection(direction);
        float X = animator.GetFloat("Look X");
        float Y = animator.GetFloat("Look Y");

        state = s;
        switch (s)
        {
            case "wall":
                //already set animator
                collider2d.enabled = true;
                break;
            case "door":
                animator.SetFloat("Look X", X * 2);
                animator.SetFloat("Look Y", Y * 2);
                collider2d.enabled = false;
                break;
            case "locked":
                animator.SetFloat("Look X", X * 3);
                animator.SetFloat("Look Y", Y * 3);
                collider2d.enabled = true;
                break;
            case "shut":
                animator.SetFloat("Look X", X * 4);
                animator.SetFloat("Look Y", Y * 4);
                collider2d.enabled = true;
                break;
            case "bombed":
                animator.SetFloat("Look X", X * 5);
                animator.SetFloat("Look Y", Y * 5);
                collider2d.enabled = false;
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (state == "locked")
        {
            LinkController controller = other.gameObject.GetComponent<LinkController>();
            Inventory inventory = other.gameObject.GetComponent<Inventory>();

            if (controller != null)
            {
                if (inventory.keys > 0)
                {
                    controller.ChangeKeyCount(-1);
                    setState("door");
                    PlaySound(open);
                    if (hasPartner)
                    {
                        partnerController.setState("door");
                    }
                }
            }
        }
        else if(state == "wall" && bombable)
        {
            ExplosiveDamage controller = other.gameObject.GetComponent<ExplosiveDamage>();

            if (controller != null)
            {
                setState("bombed");
                PlaySound(open);
                if (hasPartner)
                {
                    partnerController.setState("bombed");
                }
            }
        }
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
