using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearWhenNoEnemies : MonoBehaviour
{
    public List<GameObject> enemies;
    int numEnemies;

    Color startColor;

    SpriteRenderer spriteRenderer;
    GenericItemScript collectionScript;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collectionScript = GetComponent<GenericItemScript>();

        collectionScript.collectible = false;

        numEnemies = enemies.Count;
        startColor = spriteRenderer.color;
        spriteRenderer.color = Color.clear;
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < numEnemies; i++)
        {
            if(enemies[i] == null)
            {
                numEnemies--;
                enemies.RemoveAt(i);
            }
        }
        if(numEnemies == 0)
        {
            spriteRenderer.color = startColor;
            collectionScript.collectible = true;
        }
    }
}
