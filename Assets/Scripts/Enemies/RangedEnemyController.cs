using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyController : MonoBehaviour
{
    public float moveSpeed;
    public float projectileSpeed;

    public float moveTime;
    float moveTimer;
    public float attackTime;
    float attackTimer;

    public bool vertical;
    bool changeDirection;
    float direction = 1f;

    Vector2 lookDirection = new Vector2(0, 0);

    public GameObject projectilePrefab;
    Rigidbody2D rigidbody2d;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        moveTimer = moveTime;
        attackTimer = attackTime;

        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        moveTimer -= Time.deltaTime;
        if (moveTimer < 0)
        {
            if (changeDirection)
            {
                direction *= -1;
            }
            vertical = !vertical;
            changeDirection = !changeDirection;
            moveTimer = moveTime;
        }

        attackTimer -= Time.deltaTime;
        if (attackTimer < 0)
        {
            Launch();
            attackTimer = attackTime;
        }

        Vector2 position = rigidbody2d.position;

        if (vertical)
        {
            //position.y += direction * moveSpeed * Time.deltaTime;
            animator.SetFloat("Look X", 0);
            animator.SetFloat("Look Y", direction);
            lookDirection.x = 0;
            lookDirection.y = direction;
        }
        else
        {
            //position.x += direction * moveSpeed * Time.deltaTime;
            animator.SetFloat("Look X", direction);
            animator.SetFloat("Look Y", 0);
            lookDirection.x = direction;
            lookDirection.y = 0;
        }

        rigidbody2d.position = position;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        LinkController controller = other.gameObject.GetComponent<LinkController>();

        if (controller != null)
        {
            controller.ChangeHealth(-1);
        }
    }

    void Launch()
    {
        GameObject projectile = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.2f, Quaternion.identity);

        BoomerangProjectileController boomerangProjectile = projectile.GetComponent<BoomerangProjectileController>();
        boomerangProjectile.Launch(lookDirection, projectileSpeed);
    }
}
