using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GroundCheck : MonoBehaviour
{
    public event Action<bool> OnChangeGrounded;

    public bool IsGrounded { get; private set; }

    [SerializeField] private float _groundDistance = 0.4f;
    [SerializeField] private LayerMask _groundMask;

    private void Update()
    {
        var oldIsGrounded = IsGrounded;
        IsGrounded = Physics.CheckSphere(transform.position, _groundDistance, _groundMask);
        if (oldIsGrounded != IsGrounded)
        {
            OnChangeGrounded?.Invoke(IsGrounded);
        }
    }
}
