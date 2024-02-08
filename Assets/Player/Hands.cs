using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Hands : MonoBehaviour
{
    public event Action<bool> OnChangeUse;

    [SerializeField] private float _forceThrow = 1;

    private SelectableObject _selectableObject;

    public void Throw()
    {
        if (!_selectableObject)
        {
            return;
        }

        _selectableObject.Throw(_forceThrow);
        _selectableObject = null;
        OnChangeUse.Invoke(false);
    }

    public void Drop()
    {
        if (!_selectableObject)
        {
            return;
        }

        _selectableObject.Drop();
        _selectableObject = null;
        OnChangeUse.Invoke(false);
    }

    public void Use(SelectableObject selectableObject)
    {
        if (_selectableObject)
        {
            Drop();
            return;
        }

        OnChangeUse.Invoke(true);
        selectableObject.PickUp(transform);
        _selectableObject = selectableObject;
    }
}
