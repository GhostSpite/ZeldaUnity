using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OldManController : MonoBehaviour
{
    public float projectileSpeed;
    public GameObject projectilePrefab;

    public float attackTime;
    float attackTimer;

    public GameObject link;
    Vector2 linkPos;
    
    bool isHit;
    public bool hit { get { return isHit; } set { isHit = value; } }

    Rigidbody2D rigidbody2d;
    public AudioClip audioClip;

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        linkPos = link.transform.position;
        attackTimer = 0;
    }

    void Update()
    {
        if (isHit)
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer < 0)
            {
                Launch();
                attackTimer = attackTime;
            }
        }

        linkPos = link.transform.position;
    }

    void Launch()
    {
        Vector2 rightPos = new Vector2(rigidbody2d.position.x + 3f, rigidbody2d.position.y);
        Vector2 leftPos = new Vector2(rigidbody2d.position.x - 3f, rigidbody2d.position.y);
        GameObject projectileRight = Instantiate(projectilePrefab, rightPos, Quaternion.identity);
        GameObject projectileLeft = Instantiate(projectilePrefab, leftPos, Quaternion.identity);

        FireballProjectileController fireballProjectileRight = projectileRight.GetComponent<FireballProjectileController>();
        fireballProjectileRight.Launch(-(rightPos - linkPos), projectileSpeed);
        FireballProjectileController fireballProjectileLeft = projectileLeft.GetComponent<FireballProjectileController>();
        fireballProjectileLeft.Launch(-(leftPos - linkPos), projectileSpeed);
        link.GetComponent<LinkController>().PlaySound(audioClip);
    }
}
