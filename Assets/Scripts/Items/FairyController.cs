using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FairyController : MonoBehaviour
{
    public float speed;
    public int moveSeed;
    private int direction;
    private System.Random rand;

    public float moveTime;
    float timer;

    Rigidbody2D rigidbody2d;
    void Start()
    {
        rand = new System.Random(moveSeed);
        timer = moveTime;
        direction = 2;

        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        moveWithAI();
    }

    void moveWithAI()
    {
        changeDirection();
        Vector2 position = rigidbody2d.position;
        //just walking around, stop when attacking
        switch (direction)
        {
            case -4:
                position = moveNW(position);
                break;
            case -3:
                position = moveSW(position);
                break;
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
            case 3:
                position = moveNE(position);
                break;
            case 4:
                position = moveSE(position);
                break;
            case 0:
                break;
        }
        rigidbody2d.position = position;
    }

    void changeDirection()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            direction = rand.Next(-5, 5);
            timer = moveTime;
        }
    }

    Vector2 moveUp(Vector2 position)
    {
        position.y += speed * Time.deltaTime;
        return position;
    }

    Vector2 moveNE(Vector2 position)
    {
        position.y += speed * Time.deltaTime;
        position.x += speed * Time.deltaTime;
        return position;
    }

    Vector2 moveRight(Vector2 position)
    {
        position.x += speed * Time.deltaTime;
        return position;
    }

    Vector2 moveSE(Vector2 position)
    {
        position.x += speed * Time.deltaTime;
        position.y -= speed * Time.deltaTime;
        return position;
    }

    Vector2 moveDown(Vector2 position)
    {
        position.y -= speed * Time.deltaTime;
        return position;
    }

    Vector2 moveSW(Vector2 position)
    {
        position.y -= speed * Time.deltaTime;
        position.x -= speed * Time.deltaTime;
        return position;
    }

    Vector2 moveLeft(Vector2 position)
    {
        position.x -= speed * Time.deltaTime;
        return position;
    }

    Vector2 moveNW(Vector2 position)
    {
        position.x -= speed * Time.deltaTime;
        position.y += speed * Time.deltaTime;
        return position;
    }
}
