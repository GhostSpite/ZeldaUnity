using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableWhenNoEnemies : MonoBehaviour
{
    public List<GameObject> enemies;
    int numEnemies;

    Rigidbody2D rigidbody2d;

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        rigidbody2d.constraints = RigidbodyConstraints2D.FreezeAll;

        numEnemies = enemies.Count;
    }
    
    void Update()
    {
        for (int i = 0; i < numEnemies; i++)
        {
            if (enemies[i] == null)
            {
                numEnemies--;
                enemies.RemoveAt(i);
            }
        }
        if (numEnemies == 0)
        {
            rigidbody2d.constraints = RigidbodyConstraints2D.None;
            rigidbody2d.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
}
