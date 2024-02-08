using UnityEngine;

public class DraggableObject : SelectableObject
{
    public override void Throw(float force)
    {
        SetSelect(null);
        _rigidbody.AddForce(_hands.forward * force);
        _hands = null;
    }

    public override void Drop()
    {
        _hands = null;
        SetSelect(_hands);
    }

    public override void PickUp(Transform handsT)
    {
        _hands = handsT;
        SetSelect(_hands);
    }
}
