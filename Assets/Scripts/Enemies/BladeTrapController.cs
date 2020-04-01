using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeTrapController : MonoBehaviour
{
    public float speed;
    private int direction;
    Rigidbody2D rigidbody2d;
    //LinkController controller;
    Vector2 linkPosition;
    bool moving;
    bool returning;
    Vector2 initialPosition;
    public GameObject linkController;

    // Start is called before the first frame update
    void Start()
    {
        moving = false;
        returning = false;
        linkController = GameObject.Find("Link");
        rigidbody2d = GetComponent<Rigidbody2D>();
        initialPosition = rigidbody2d.position;
    }

    // Update is called once per frame
    void Update()
    {
        linkPosition = linkController.transform.position;
        Vector2 position = rigidbody2d.position;
        position = checkPosition(position);
        rigidbody2d.position = position;
    }

    Vector2 checkPosition(Vector2 position)
    {
        if ((int)linkPosition.y == (int)position.y && !moving)
        {
            if (linkPosition.x < position.x)
            {
                direction = -1;
            }
            else
            {
                direction = 1;
            }
            moving = true;
        }
        else if ((int)linkPosition.x == (int)position.x && !moving)
        {
            if (linkPosition.y < position.y)
            {
                direction = 2;
            }
            else
            {
                direction = -2;
            }
            moving = true;
        }
        else if(moving)
        {
            position = charge(position);
        }

        if ((int)position.x == (int)initialPosition.x && (int)position.y == (int)initialPosition.y && returning)
        {
            position.x = initialPosition.x;
            position.y = initialPosition.y;
            moving = false;
            speed = speed * 4;
            returning = false;
        }
        return position;
    }

    Vector2 charge(Vector2 position)
    {
        switch (direction)
        {
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
        }
        return position;
    }

    Vector2 moveUp(Vector2 position)
    {
        position.y += speed * Time.deltaTime;
        return position;
    }

    Vector2 moveDown(Vector2 position)
    {
        position.y -= speed * Time.deltaTime;
        return position;
    }

    Vector2 moveRight(Vector2 position)
    {
        position.x += speed * Time.deltaTime;
        return position;
    }

    Vector2 moveLeft(Vector2 position)
    {
        position.x -= speed * Time.deltaTime;
        return position;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        LinkController controller = other.gameObject.GetComponent<LinkController>();
        if (controller != null)
        {
            controller.ChangeHealth(-2);
        }
        else if(!returning)
        {
            direction = -1 * direction;
            speed = speed / 4;
            returning = true;
        }
    }
}
