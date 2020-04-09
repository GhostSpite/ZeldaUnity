using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCollision : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name.Contains("Aquamentus"))
        {
            AquamentusController cont1 = col.GetComponent<AquamentusController>();
            if (cont1.health > 0)
            {
                cont1.DamageAquamentus(-1);
            }
            if(cont1.health <= 0) 
            {
                Destroy(col.gameObject);
            }
            Debug.Log("hit aqua " + "health: " + cont1.health + "/" + cont1.maxHealth);
        }else if (col.gameObject.name.Contains("Gel"))
        {
            GelController cont2 = col.GetComponent<GelController>();
            if (cont2.health > 0)
            {
                cont2.ChangeHealth(-1);
            }
            Debug.Log("hit gel " + "health: " + cont2.health + "/" + cont2.maxHealth);
        }else if (col.gameObject.name.Contains("Goriya"))
        {
            GoriaController cont3 = col.GetComponent<GoriaController>();
            if (cont3.health > 0)
            {
                cont3.DamageGoria(-1);
            }
            if (cont3.health <= 0)
            {
                Destroy(col.gameObject);
            }
            Debug.Log("hit goria " + "health: " + cont3.health + "/" + cont3.maxHealth);
        }else if (col.gameObject.name.Contains("Keese"))
        {
            KeeseController cont4 = col.GetComponent<KeeseController>();
            if (cont4.health > 0)
            {
                cont4.ChangeHealth(-1);
            }
            Debug.Log("hit keese " + "health: " + cont4.health + "/" + cont4.maxHealth);
        }else if (col.gameObject.name.Contains("Skeleton"))
        {
            StalfosController cont5 = col.GetComponent<StalfosController>();
            if (cont5.health > 0)
            {
                cont5.DamageStalfos(-1);
            }
            if (cont5.health <= 0)
            {
                Destroy(col.gameObject);
            }
            Debug.Log("hit stalfos " + "health: " + cont5.health + "/" + cont5.maxHealth);
        }
    }
}
