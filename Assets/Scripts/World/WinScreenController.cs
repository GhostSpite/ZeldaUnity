using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScreenController : MonoBehaviour
{
    public bool won;
    public bool done;
    Rigidbody2D rigidBody;

    //float delay;
    //float timer;
    float speed;

    void Start()
    {
        won = false;
        done = false;
        rigidBody = GetComponent<Rigidbody2D>();
        //delay = 1f;
        //timer = 0f;
        speed = 1f;
    }
    
    void Update()
    {
        if (won & !done)
        {
            rigidBody.position = new Vector2(rigidBody.position.x, rigidBody.position.x - speed * Time.deltaTime);
            if (rigidBody.position.y < 43.5)//GetComponent<Transform>().position.y < -1.5)
            {
                rigidBody.position = new Vector2(rigidBody.position.x, 43.5f);
                done = true;
            }
        }
    }
}
