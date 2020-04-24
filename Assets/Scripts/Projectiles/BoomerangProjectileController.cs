using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangProjectileController : MonoBehaviour
{
    public string collisionTag;
    Rigidbody2D rigidbody2d;
    AudioSource audioSource;
    public AudioClip fly;
    public AudioClip getHit;
    public AudioClip die;
    float time = 1f;//0.5f;
    float timer;
    float speed= 7f;
    float rotation = 0f;

    Vector2 direction;

    // Start is called before the first frame update
    void Awake()
    {
        rigidbody2d = gameObject.GetComponent<Rigidbody2D>();
        audioSource = gameObject.GetComponent<AudioSource>();
        //animator = gameObject.GetComponent<Animator>();

        PlaySound(fly);
        timer = 2 * time;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        rotation += 5;
        if (rotation > 360) rotation = rotation % 360;
        rigidbody2d.MoveRotation(rotation);

        Vector2 position = rigidbody2d.position;

        if (timer > time) {
            MoveAway(position);
        } 
        else if (timer > 0){
            MoveBack(position);
        } 
        else{
            LinkController.boomerangPresent = false;
            Destroy(gameObject);
        }
    }


    public void MoveAway(Vector2 position)
    {
        position += direction * speed * Time.deltaTime;
        rigidbody2d.position = position;
    }

    public void MoveBack(Vector2 position)
    {
        position += -1 * direction * speed * Time.deltaTime;
        rigidbody2d.position = position;
    }

    public void Launch(Vector2 direction, float speed)
    {
        this.direction = direction;
        this.speed = speed;

        //rigidbody2d.angularVelocity = 360f;

        //animator.SetFloat("Look X", direction.x);
        //animator.SetFloat("Look Y", direction.y);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(collisionTag)){
            if(collisionTag == "Enemy")
            {
                EnemyReaction(other.gameObject);
            }
            else
            {
                LinkController controller = other.gameObject.GetComponent<LinkController>();

                if (controller != null)
                {
                    controller.ChangeHealth(-1);
                }
            }
            Destroy(gameObject);
        }
        else{
            timer = (2*time) -timer -(float) 0.1;
        }
    }

    void EnemyReaction(GameObject enemy)
    {
        if (enemy.name.Contains("Gel"))
        {
            GelController controller = enemy.GetComponent<GelController>();
            if (controller.health > 0)
            {
                controller.ChangeHealth(-1);
                if (controller.health > 0)
                {
                    PlayEnemySound(getHit);
                }
                else
                {
                    PlayEnemySound(getHit);
                    PlayEnemySound(die);
                }
            }
        }
        else if (enemy.name.Contains("Keese"))
        {
            KeeseController controller = enemy.GetComponent<KeeseController>();
            if (controller.health > 0)
            {
                controller.ChangeHealth(-1);
                if (controller.health > 0)
                {
                    PlayEnemySound(getHit);
                }
                else
                {
                    PlayEnemySound(getHit);
                    PlayEnemySound(die);
                }
            }
        }
        else if (enemy.name.Contains("Skeleton"))
        {
            StalfosController controller = enemy.GetComponent<StalfosController>();
            if (!controller.freeze)
            {
                controller.freeze = true;
            }

        }
        else if (enemy.name.Contains("Goriya"))
        {
            GoriaController controller = enemy.GetComponent<GoriaController>();
            if (!controller.freeze)
            {
                controller.freeze = true;
            }
        }
        else if (enemy.name.Contains("Wallmaster"))
        {
            WallmasterController controller = enemy.GetComponent<WallmasterController>();
            if (!controller.freeze)
            {
                controller.freeze = true;
            }
        }
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    public void PlayEnemySound(AudioClip clip)
    {
        Debug.Log("sound");
        audioSource.PlayOneShot(clip);
        GetComponent<Renderer>().enabled = false;
        GetComponent<Collider2D>().isTrigger = true;
        Destroy(gameObject, clip.length);
    }
}
