using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    private float _currentHealth;
    [SerializeField] private float maxHealth;
    [SerializeField] private Collider _collider;
    private Animator _animator;
    private Rigidbody _rb;
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _currentHealth = maxHealth;
        _rb = GetComponent<Rigidbody>();
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
        _collider.enabled = false;
        _rb.isKinematic = true;
        GetComponent<CatView>().enabled = false;
        _animator.SetBool("Die", true);
        Destroy(gameObject, 3f);
    }
}
