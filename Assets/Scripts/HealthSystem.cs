using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem 
{
    int maxHealth;
    int currentHealth;
    bool isAlive;

    // Constructor
    public HealthSystem(int maxHealth)
    {
        this.maxHealth = maxHealth;
        currentHealth = maxHealth;
        isAlive = true;
    }

    public void Kill()
    {
        this.isAlive = false;
    }

    public bool IsAlive()
    {
        return isAlive;
    }

    public int GetHealth()
    {
        return currentHealth;
    }

    public void Heal(int amount)
    {
        if (isAlive && currentHealth < maxHealth)
        {
            currentHealth += amount;
        }
        else
        {
            Debug.Log("Entity cannot be healed!");
        }
    }

    public void TakeDamage(int amount)
    {
        if (isAlive)
        {
            currentHealth -= amount;
        }

        if(currentHealth <= 0)
        {
            currentHealth = 0;
            isAlive = false; 
        }
    }
}
