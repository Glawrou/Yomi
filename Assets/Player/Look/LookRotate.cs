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

    public bool IsSit => _head.IsSit;

    [SerializeField] private ClampData _clampYCamera;
    [SerializeField] private Head _head;

    private const float _distanceDetected = 4;
    private const float _distanceDetectedMonsters = 100;
    private const string MonsterTag = "Monster";

    private float _xRotation = 0;
    private RaycastHit _hit;

    public void SetSit(bool isActive) => _head.Sit(isActive);

    public void LockAt(Transform transform)
    {
        _head.transform.LookAt(transform);
    }

    public void MouseInputHandler(Vector2 vector)
    {
        transform.Rotate(Vector3.up * vector.x);
        _xRotation -= vector.y;
        _xRotation = Mathf.Clamp(_xRotation, _clampYCamera.Min, _clampYCamera.Max);
        _head.transform.localRotation = Quaternion.Euler(Vector3.right * _xRotation);
        DetectedObject();
        DetectedObjectMonster();
    }

    private void DetectedObject()
    {
        var ray = new Ray(_head.transform.position, _head.transform.forward * _distanceDetected);
        Physics.Raycast(ray, out _hit, _distanceDetected);
    }

    private void DetectedObjectMonster()
    {
        var ray = new Ray(_head.transform.position, _head.transform.forward * _distanceDetectedMonsters);
        Physics.Raycast(ray, out var hit, _distanceDetectedMonsters);
        if (hit.collider && hit.collider.tag == MonsterTag)
        {
            hit.collider.GetComponent<Child>().Out();
        }
    }
}
