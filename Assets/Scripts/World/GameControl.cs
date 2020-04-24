using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    public KeyCode next;
    public KeyCode prev;

    public float offset;
    GameObject room;
    GameObject player;

    Camera cam;
    Vector3 roomLoc;
    Vector3 playerLoc;
    int index = 0;

    static bool start = false;

    private void Start()
    {
        cam = Camera.main;
        if (!start)
        {
            SceneManager.LoadScene(0);
            start = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SceneManager.LoadScene(0);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (Input.GetKeyDown(next) || Input.GetKeyDown(prev))
        {
            CycleRooms();
        }
    }

    void CycleRooms()
    {

        room = gameObject.transform.GetChild(0).gameObject;

        if (Input.GetKeyDown(next))
        {
            index++;
            if (index >= room.transform.childCount) index = 0;
        }

        if (Input.GetKeyDown(prev))
        {
            index--;
            if (index < 0) index = room.transform.childCount - 1;
        }

        roomLoc = room.transform.GetChild(index).transform.position;
        roomLoc.y += offset;
        roomLoc.z = cam.transform.position.z;

        playerLoc = room.transform.GetChild(index).transform.position;
        playerLoc.y += -4f;
        if (index + 1 == room.transform.childCount) playerLoc.y += 1f;
        playerLoc.z = 0f;

        player = gameObject.transform.GetChild(1).gameObject;
        player.transform.position = playerLoc;
        cam.transform.position = roomLoc;
    }
}
