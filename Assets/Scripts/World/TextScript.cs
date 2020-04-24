using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextScript : MonoBehaviour
{

    string deathText = "   GAME OVER \n\nCONTINUE ( c )\n   RETRY ( R )";
    public GameObject textObject;

    public void PrintDeathText()
    {
        textObject.gameObject.SetActive(true);
        textObject.GetComponent<Text>().text = deathText;
    }

    public void DeleteText()
    {
        textObject.gameObject.SetActive(false);
    }
}
