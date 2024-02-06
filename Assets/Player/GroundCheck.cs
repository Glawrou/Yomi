using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public bool IsGround { get; private set; }

    [SerializeField] private float _groundDistance = 0.4f;
    [SerializeField] private LayerMask _groundMask;

    private void Update()
    {
        IsGround = Physics.CheckSphere(transform.position, _groundDistance, _groundMask);
    }
}
