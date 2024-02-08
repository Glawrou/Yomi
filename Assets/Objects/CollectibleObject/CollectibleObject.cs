using UnityEngine;

public class CollectibleObject : SelectableObject
{
    public override void Drop()
    {
        Destroy(gameObject);
    }

    public override void PickUp(Transform handsT)
    {
        _hands = handsT;
        SetSelect(_hands);
    }

    public override void Throw(float force)
    {
        Destroy(gameObject);
    }
}
