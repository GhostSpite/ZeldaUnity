using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int maxHealth;
    public int health;
    public int numberOfHearts;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;

    void Update()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            maxHealth = numberOfHearts * 2;
            UpdateContainers(i);

            if ((2 * i) + 2 <= health)
            {
                hearts[i].sprite = fullHeart;
            }
            else if ((2 * i) + 1 == health)
            {
                hearts[i].sprite = halfHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }

    void UpdateContainers(int i)
    {
        if (i < numberOfHearts)
        {
            hearts[i].enabled = true;
        }
        else
        {
            hearts[i].enabled = false;
        }
    }
}
