using System;
using UnityEngine;

public class LookRotate : MonoBehaviour
{
    public GameObject Hit 
    { 
        get 
        {
            return _hit.collider?.gameObject;
        }
    }

    [SerializeField] private ClampData _clampYCamera;
    [SerializeField] private Head _head;

    private const float _distanceDetected = 3;
    private float _xRotation = 0;
    private RaycastHit _hit;

    public void SetSit(bool isActive) => _head.Sit(isActive);

    public void MouseInputHandler(Vector2 vector)
    {
        transform.Rotate(Vector3.up * vector.x);
        _xRotation -= vector.y;
        _xRotation = Mathf.Clamp(_xRotation, _clampYCamera.Min, _clampYCamera.Max);
        _head.transform.localRotation = Quaternion.Euler(Vector3.right * _xRotation);
        DetectedObject();
    }

    private void DetectedObject()
    {
        var ray = new Ray(_head.transform.position, _head.transform.forward * _distanceDetected);
        Physics.Raycast(ray, out _hit, _distanceDetected);
    }
}