using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMenu : MonoBehaviour
{
    public GameObject inventoryCanvas;

    bool isPaused;
    public KeyCode inventoryKey
        ;

    void Start()
    {
        inventoryCanvas.SetActive(false);
        isPaused = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(inventoryKey))
        {
            ChangeInventory();
        }
    }

    void ChangeInventory()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            inventoryCanvas.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            inventoryCanvas.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}
