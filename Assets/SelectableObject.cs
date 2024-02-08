using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableObject : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;

    private Transform _hands;

    public void Throw(float force)
    {
        SetSelect(null);
        _rigidbody.AddForce(_hands.forward * force);
        _hands = null;
    }

    public void Drop()
    {
        _hands = null;
        SetSelect(_hands);
    }

    public void PickUp(Transform handsT)
    {
        _hands = handsT;
        SetSelect(_hands);
    }

    private void SetSelect(Transform handsT)
    {
        var hands = handsT != null;
        _rigidbody.isKinematic = hands;
        transform.parent = hands ? handsT : null;
        transform.position = hands ? handsT.position : transform.position;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
    }
}
