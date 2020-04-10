using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearWhenNoEnemies : MonoBehaviour
{
    public List<GameObject> enemies;
    int numEnemies;

    Color startColor;
    bool invisible;

    public AudioClip appear;

    AudioSource audioSource;
    SpriteRenderer spriteRenderer;
    GenericItemScript collectionScript;
    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collectionScript = GetComponent<GenericItemScript>();
        audioSource = GetComponent<AudioSource>();

        collectionScript.collectible = false;
        invisible = true;

        numEnemies = enemies.Count;
        startColor = spriteRenderer.color;
        spriteRenderer.color = Color.clear;
    }
    
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
        if(numEnemies == 0 && invisible)
        {
            spriteRenderer.color = startColor;
            collectionScript.collectible = true;
            PlaySound(appear);
            invisible = false;
        }
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
