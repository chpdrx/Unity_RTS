using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceTakeDamage : ITakeDamage
{
    public float OnTakeDamage(float health, float armor, float damage)
    {
        float currentHealth = health - 1;
        Debug.Log(currentHealth);
        if (health <= 0) OnDeath();
        return currentHealth;
    }

    public void OnDeath()
    {
        Debug.Log("Resource is empty!");
    } 
}
