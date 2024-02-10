using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockAt : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset;

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    private void Update()
    {
        if (!_target)
        {
            return;
        }

        transform.LookAt(_target.position + _offset);
    }
}
