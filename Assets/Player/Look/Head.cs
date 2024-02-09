using UnityEngine;

public class Head : MonoBehaviour 
{
    public bool IsSit { get; private set; }

    [SerializeField] private float _normalPosition = 1;
    [SerializeField] private float _sitPosition = 0.6f;

    public void Sit(bool isActive)
    {
        IsSit = isActive;
        var pos = Vector3.up * (IsSit ? _sitPosition : _normalPosition);
        transform.localPosition = pos;
    }
}
