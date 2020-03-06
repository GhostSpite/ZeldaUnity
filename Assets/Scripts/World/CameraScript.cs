using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    
    Vector3 target;
    public float smoothing;

    public void Move(Vector3 change)
    {
        target = new Vector3(transform.position.x + change.x, transform.position.y + change.y, transform.position.z + change.z);
        transform.position = target;
    }
}
