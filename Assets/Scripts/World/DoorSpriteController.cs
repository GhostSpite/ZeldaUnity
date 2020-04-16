using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSpriteController : MonoBehaviour
{
    public string direction;
    public string state;
    public bool bombable;
    public bool trigger;
    public bool openForEnemyDeath;
    public List<GameObject> enemies;
    int numEnemies;

    public AudioClip open;

    AudioSource audioSource;
    Animator animator;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();

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
                }
            }
            else if (trigger)
            {

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
                //already set
                break;
            case "door":
                animator.SetFloat("Look X", X * 2);
                animator.SetFloat("Look Y", Y * 2);
                break;
            case "locked":
                animator.SetFloat("Look X", X * 3);
                animator.SetFloat("Look Y", Y * 3);
                break;
            case "shut":
                animator.SetFloat("Look X", X * 4);
                animator.SetFloat("Look Y", Y * 4);
                break;
            case "bombed":
                animator.SetFloat("Look X", X * 5);
                animator.SetFloat("Look Y", Y * 5);
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
                }
            }
        }
        else if(state == "wall" && bombable)
        {
            LiveBomb controller = other.gameObject.GetComponent<LiveBomb>();

            if (controller != null)
            {
                setState("bombed");
                PlaySound(open);
            }
        }
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
