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
    Key keyScript;
    Equipment boomScript;
    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        keyScript = GetComponent<Key>();
        boomScript = GetComponent<Equipment>();
        audioSource = GetComponent<AudioSource>();

        if (keyScript != null)
        {
            keyScript.collectible = false;
        }
        else
        {
            boomScript.collectible = false;
        }

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
            PlaySound(appear);
            invisible = false;
            if(keyScript != null)
            {
                keyScript.collectible = true;
            }
            else
            {
                boomScript.collectible = true;
            }
        }
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
