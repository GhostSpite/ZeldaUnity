using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryMenu : MonoBehaviour
{
    public GameObject player;
    public GameObject inventoryCanvas;

    public Image map;
    public Image compass;

    public Image secondary;
    Sprite sprite;

    Inventory inventory;

    bool isPaused;
    public KeyCode inventoryKey
        ;

    void Start()
    {
        inventory = player.GetComponent<Inventory>();
        inventoryCanvas.SetActive(false);
        isPaused = false;
    }

    void Update()
    {
        UpdateEquipment();
        

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

    void UpdateEquipment()
    {
        if (inventory.secActive != Inventory.Secondary.NONE)
        {
            secondary.enabled = true;
            secondary.sprite = inventory.secondary.sprite;
        }
        else
        {
            secondary.enabled = false;
        }

        if (inventory.hasCompass)
        {
            compass.enabled = true;
        }
        else
        {
            compass.enabled = false;
        }

        if (inventory.hasMap)
        {
            map.enabled = true;
        }
        else
        {
            map.enabled = false;
        }

    }
}
