using UnityEngine;
using System;

public class CollectibleObject : SelectableObject
{
    public event Action<CollectibleObject> OnCollect;

    public override void Drop()
    {
        Collect();
    }

    public override void PickUp(Transform handsT)
    {
        _hands = handsT;
        SetSelect(_hands);
    }

    public override void Throw(float force)
    {
        Collect();
    }

    public void Collect()
    {
        OnCollect?.Invoke(this);
        Destroy(gameObject);
    }
}
