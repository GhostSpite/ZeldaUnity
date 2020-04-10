using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    
    Vector3 target;
    public float smoothing;
    public SpriteRenderer walls;

    void Start()
    {
        //May be a way to get the screen to be square like the original game
        //Camera.main.rect = new Rect(transform.position.x, transform.position.y, walls.bounds.size.x, walls.bounds.size.y);
    }

    public void Move(Vector3 change)
    {
        target = new Vector3(transform.position.x + change.x, transform.position.y + change.y, transform.position.z + change.z);
        transform.position = target;

        //Smooth transition
        //transform.position = Vector3.Lerp(transform.position, target, smoothing);
    }

}
