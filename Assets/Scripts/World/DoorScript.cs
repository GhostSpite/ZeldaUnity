using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public Vector3 camChange; 
    public Vector2 playerChange;
    CameraScript cam;

    private void Start()
    {
        cam = Camera.main.GetComponent<CameraScript>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        LinkController controller = other.gameObject.GetComponent<LinkController>();

        if (controller != null)
        {
            cam.Move(camChange);
            controller.RoomChange(playerChange);
        }
    }

}
