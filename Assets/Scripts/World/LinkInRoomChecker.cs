using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkInRoomChecker : MonoBehaviour
{
    public List<GameObject> enemies;
    public bool inRoom;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.Contains("Link"))
        {
            if (!inRoom)
            {
                inRoom = true;
                for (int i = 0; i < enemies.Count; i++)
                {
                    enemies[i].GetComponent<BladeTrapController>().isInRoom = true;
                }
            }
            else
            {
                inRoom = false;
                for (int i = 0; i < enemies.Count; i++)
                {
                    enemies[i].GetComponent<BladeTrapController>().isInRoom = true;
                }
            }
        }
    }
}
