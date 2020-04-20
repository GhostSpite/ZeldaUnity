using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OldManController : MonoBehaviour
{
    public float projectileSpeed;
    public GameObject projectilePrefab;

    public GameObject link;
    Vector2 linkPos;

    public string message;
    public GameObject textObject;
    public float delay;

    bool activated;

    bool isHit;
    public bool hit { set { isHit = value; } }

    public AudioClip beep;

    AudioSource audioSource;
    Rigidbody2D rigidbody2d;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        linkPos = link.transform.position;

        activated = false;
    }

    void Update()
    {
        if (isHit)
        {
            Launch();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        LinkController controller = other.gameObject.GetComponent<LinkController>();

        if (controller != null && !activated)
        {
            StartCoroutine(Type());
            activated = true;
        }
    }

    IEnumerator Type()
    {
        Text text = textObject.GetComponent<Text>();
        for (int i = 0; i <= message.Length; i++)
        {
            text.text  = message.Substring(0,i);
            PlaySound(beep);
            yield return new WaitForSeconds(delay);
        }
    }

    IEnumerator Launch()
    {
        Vector2 rightPos = new Vector2(rigidbody2d.position.x + 3f, rigidbody2d.position.y);
        Vector2 leftPos = new Vector2(rigidbody2d.position.x + 3f, rigidbody2d.position.y);
        GameObject projectileRight = Instantiate(projectilePrefab, rightPos, Quaternion.identity);
        GameObject projectileLeft = Instantiate(projectilePrefab, leftPos, Quaternion.identity);

        FireballProjectileController fireballProjectileRight = projectileRight.GetComponent<FireballProjectileController>();
        fireballProjectileRight.Launch(rightPos - linkPos, projectileSpeed);
        FireballProjectileController fireballProjectileLeft = projectileLeft.GetComponent<FireballProjectileController>();
        fireballProjectileLeft.Launch(leftPos - linkPos, projectileSpeed);
        yield return new WaitForSeconds(2);
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
