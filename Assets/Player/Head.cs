using UnityEngine;

public class Head : MonoBehaviour 
{
    [SerializeField] private float _normalPosition = 1;
    [SerializeField] private float _sitPosition = 0.6f;

    public void Sit(bool isActive)
    {
        transform.localPosition = Vector3.up * (isActive ? _sitPosition : _normalPosition);
    }
}
