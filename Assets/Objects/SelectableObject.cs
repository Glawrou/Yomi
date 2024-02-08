using UnityEngine;

public abstract class SelectableObject : MonoBehaviour
{
    [SerializeField] protected Rigidbody _rigidbody;
    [SerializeField] private int _itemsHands;
    [SerializeField] private GameObject[] _objectsLayer;

    protected Transform _hands;

    public abstract void Throw(float force);
    public abstract void Drop();
    public abstract void PickUp(Transform handsT);

    protected void SetSelect(Transform handsT)
    {
        var hands = handsT != null;
        foreach (var item in _objectsLayer)
        {
            item.layer = hands ? _itemsHands : 0;
        }
        
        _rigidbody.isKinematic = hands;
        transform.parent = hands ? handsT : null;
        transform.position = hands ? handsT.position : transform.position;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
    }
}
