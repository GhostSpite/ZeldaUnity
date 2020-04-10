using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemUponDeath : MonoBehaviour
{
    public GameObject keyPrefab;
    public GameObject rupeePrefab;
    public GameObject heartPrefab;
    public GameObject bombPrefab;
    public GameObject clockPrefab;
    public GameObject fairyPrefab;
    GameObject drop;

    public void dropItem(bool hasKey, int seed, Vector2 position)
    {
        System.Random rand = new System.Random(seed);
        if (!hasKey)
        {
            int item = rand.Next(0, 30);
            if(item <= 4)
            {
                drop = Instantiate(rupeePrefab, position, Quaternion.identity);
            }
            else if (item <= 8)
            {
                drop = Instantiate(heartPrefab, position, Quaternion.identity);
            }
            else if (item <= 12)
            {
                drop = Instantiate(bombPrefab, position, Quaternion.identity);
            }
            else if (item <= 14)
            {
                drop = Instantiate(clockPrefab, position, Quaternion.identity);
            }
            else if (item == 15)
            {
                drop = Instantiate(fairyPrefab, position, Quaternion.identity);
            }
        }
        else
        {
            drop = Instantiate(keyPrefab, position, Quaternion.identity);
        }
    }
}
