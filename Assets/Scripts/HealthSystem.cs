using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem 
{
    int _maxHealth;
    int _currentHealth;
    bool _isAlive;

    // Constructor
    public HealthSystem(int maxHealth)
    {
        this._maxHealth = maxHealth;
        _currentHealth = maxHealth;
        _isAlive = true;
    }

    public void Kill()
    {
        this._isAlive = false;
    }

    public bool IsAlive()
    {
        return _isAlive;
    }

    public int GetHealth()
    {
        return _currentHealth;
    }

    public void Heal(int amount)
    {
        if (_isAlive && _currentHealth < _maxHealth)
        {
            _currentHealth += amount;
            if(_currentHealth >= _maxHealth)
            {
                _currentHealth = _maxHealth;
            }
        }
        else
        {
            Debug.Log("Entity cannot be healed!");
        }
    }

    public void TakeDamage(int amount)
    {
        if (_isAlive)
        {
            _currentHealth -= amount;
        }

        if(_currentHealth <= 0)
        {
            _currentHealth = 0;
            _isAlive = false; 
        }
    }
}
