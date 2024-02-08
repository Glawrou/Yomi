using System;
using UnityEngine;

public class LookRotate : MonoBehaviour
{
    [SerializeField] private ClampData _clampYCamera;
    [SerializeField] private Head _head;

    private float _xRotation = 0;

    public void SetSit(bool isActive) => _head.Sit(isActive);

    public void MouseInputHandler(Vector2 vector)
    {
        transform.Rotate(Vector3.up * vector.x);
        _xRotation -= vector.y;
        _xRotation = Mathf.Clamp(_xRotation, _clampYCamera.Min, _clampYCamera.Max);
        _head.transform.localRotation = Quaternion.Euler(Vector3.right * _xRotation);
    }
}
