using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inventory : MonoBehaviour
{
    public TextMeshProUGUI[] text;

    public int rupees;
    public int keys;
    public int bombs;

    public int bait;
    public int bluePotions;
    public int redPotions;

    public bool hasRaft;
    public bool hasBow;
    public bool hasRang;
    public bool hasRod;
    public bool hasCandle;
    public bool hasFlute;
    
    void Update()
    {
        text[0].text = "   x " + rupees.ToString();
        text[1].text = "   x " + keys.ToString();
        text[2].text = "   x " + bombs.ToString();
    }

}
