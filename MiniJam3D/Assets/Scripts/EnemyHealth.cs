using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    private float _currentHealth;
    [SerializeField] private float maxHealth;

    private void Start()
    {
        _currentHealth = maxHealth;
    }

    public void TakeDamage(float hitDamageAmount)
    {
        _currentHealth -= maxHealth;
        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
