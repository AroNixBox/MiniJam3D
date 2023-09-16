using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Combat : MonoBehaviour
{
    private bool _isAttacking = false;
    private Animator _animator;
    private static readonly int IsAttacking = Animator.StringToHash("isAttacking");

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnAttack(InputValue value)
    {
        _isAttacking = value.isPressed; 
        Debug.Log($"isAttacking is {_isAttacking}");
    }
    void Update()
    {
        if (_isAttacking)
        {
            _animator.SetBool(IsAttacking, true);
        }
        else
        {
            _animator.SetBool(IsAttacking, false);
        }
    }
}
