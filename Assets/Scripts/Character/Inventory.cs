using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public enum Secondary 
    {
        BOW,
        RANG,
        BOMB,
        REDPOT, 
        BLUEPOT,
        CANDLE,
        NONE
    }

    public enum Primary
    {
        WOOD,
        MAGIC,
        NONE
    }

    public Image secondary;
    public Sprite[] secWeapons;

    public Image primary;
    public Sprite[] priWeapons;

    public RawImage map;

    public Secondary secActive;
    public Primary priActive;
    public TextMeshProUGUI[] text;

    public int rupees;
    public int keys;
    public int bombs;

    public int bait;
    public int bluePotions;
    public int redPotions;
    public int triforce;

    public bool hasMap;
    public bool hasCompass;
    public bool hasBow;
    public bool hasRang;
    public bool hasCandle;

    public float mouseTime;
    float mouseTimer;
    bool clickTooFast;

    void Start()
    {
        map.enabled = false;
        secActive = Secondary.NONE;
        secondary.enabled = false;
        priActive = Primary.WOOD;
        clickTooFast = false;
    }

    void Update()
    {
        text[0].text = "   x " + rupees.ToString();
        text[1].text = "   x " + keys.ToString();
        text[2].text = "   x " + bombs.ToString();

        CycleSecondary();

        UpdatePrimaryImage();
        UpdateSecondaryImage();


        if (mouseTimer > 0)
        {
            mouseTimer -= Time.deltaTime;
        }
        else
        {
            clickTooFast = false;
        }
    }

    void UpdateSecondaryImage()
    {
        if(secActive != Secondary.NONE)
        {
            secondary.enabled = true;
            secondary.sprite = secWeapons[(int)secActive];
        }
        else
        {
            secondary.enabled = false;
        }
        
    }

    void UpdatePrimaryImage()
    {
        if(priActive != Primary.NONE)
        {
            primary.enabled = true;
            primary.sprite = priWeapons[(int)priActive];
        }
        else
        {
            primary.enabled = false;
        }
        
    }

    void CycleSecondary()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && hasBow) secActive = Secondary.BOW;
        if (Input.GetKeyDown(KeyCode.Alpha2) && hasRang) secActive = Secondary.RANG;
        if (Input.GetKeyDown(KeyCode.Alpha3) && bombs > 0) secActive = Secondary.BOMB;
        if (Input.GetMouseButton(2) & !clickTooFast)
        {
            if (secActive == Secondary.BOW && hasRang)
            {
                secActive = Secondary.RANG;
            }
            else if (secActive == Secondary.BOW && bombs > 0)
            {
                secActive = Secondary.BOMB;
            }
            else if (secActive == Secondary.RANG && bombs > 0)
            {
                secActive = Secondary.BOMB;
            }
            else if (secActive == Secondary.RANG && hasBow)
            {
                secActive = Secondary.BOW;
            }
            else if (secActive == Secondary.BOMB && hasBow)
            {
                secActive = Secondary.BOW;
            }
            else if (secActive == Secondary.BOMB && hasRang)
            {
                secActive = Secondary.RANG;
            }
            clickTooFast = true;
            mouseTimer = mouseTime;
        }
    }
}
