using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssenceGravity : MonoBehaviour
{
    [SerializeField] private GroundCheck _groundCheck;

    private CharacterController _characterController;

    public const float GravityValue = -0.1f;
    public const float StartVelocity = -0.01f;

    private float _velocity = 0;

    public void Initialization(CharacterController characterController)
    {
        _characterController = characterController;
    }

    public void SetVelocity(float value)
    {
        _velocity = value;
    }

    public void UpdateHandler()
    {
        _characterController.Move(Vector3.up * _velocity);
        if (_groundCheck.IsGrounded && _velocity < StartVelocity)
        {
            _velocity = StartVelocity;
            return;
        }

        _velocity += GravityValue * Time.deltaTime;
    }
}
