using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDamage : MonoBehaviour
{
    public int damageEnemy(Collider2D col)
    {
        int remainingHealth = -1;
        if (col.gameObject.name.Contains("Aquamentus"))
        {
            AquamentusController cont1 = col.GetComponent<AquamentusController>();
            if (cont1.health > 0)
            {
                cont1.ChangeHealth(-1);
                remainingHealth = cont1.health;
            }
            Debug.Log("hit aqua " + "health: " + cont1.health + "/" + cont1.maxHealth);
        }
        else if (col.gameObject.name.Contains("Gel"))
        {
            GelController cont2 = col.GetComponent<GelController>();
            if (cont2.health > 0)
            {
                cont2.ChangeHealth(-1);
                remainingHealth = cont2.health;
            }
            Debug.Log("hit gel " + "health: " + cont2.health + "/" + cont2.maxHealth);
        }
        else if (col.gameObject.name.Contains("Goriya"))
        {
            GoriaController cont3 = col.GetComponent<GoriaController>();
            if (cont3.health > 0)
            {
                cont3.ChangeHealth(-1);
                remainingHealth = cont3.health;
            }
            Debug.Log("hit goria " + "health: " + cont3.health + "/" + cont3.maxHealth);
        }
        else if (col.gameObject.name.Contains("Keese"))
        {
            KeeseController cont4 = col.GetComponent<KeeseController>();
            if (cont4.health > 0)
            {
                cont4.ChangeHealth(-1);
                remainingHealth = cont4.health;
            }
            Debug.Log("hit keese " + "health: " + cont4.health + "/" + cont4.maxHealth);
        }
        else if (col.gameObject.name.Contains("Skeleton"))
        {
            StalfosController cont5 = col.GetComponent<StalfosController>();
            if (cont5.health > 0)
            {
                cont5.ChangeHealth(-1);
                remainingHealth = cont5.health;
            }
            Debug.Log("hit stalfos " + "health: " + cont5.health + "/" + cont5.maxHealth);
        }
        else if (col.gameObject.name.Contains("Wallmaster"))
        {
            WallmasterController cont6 = col.GetComponent<WallmasterController>();
            if (cont6.health > 0)
            {
                cont6.ChangeHealth(-1);
                remainingHealth = cont6.health;
            }
            Debug.Log("hit wallmaster " + "health: " + cont6.health + "/" + cont6.maxHealth);
        }
        return remainingHealth;
    }
}
