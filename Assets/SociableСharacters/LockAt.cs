using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockAt : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset;

    private void Update()
    {
        transform.LookAt(_target.position + _offset);
    }
}
