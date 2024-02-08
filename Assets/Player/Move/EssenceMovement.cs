using UnityEngine;
using System;
using System.Collections;

public class EssenceMovement : MonoBehaviour
{
    public event Action<float> OnChangeStamina;
    public event Action<bool> OnChangeFly;
    public event Action<bool> OnChangeRun;

    [SerializeField] private EssenceGravity _gravity;
    [SerializeField] public Stamina _stamina;
    [SerializeField] public GroundCheck _groundCheck;

    private CharacterController _characterController;

    [Header("Speed ")]
    [SerializeField, Range(0.001f, 0.1f)] private float _speedNormal = 0.01f;
    [SerializeField, Range(0.001f, 0.1f)] private float _speedRun = 0.018f;
    [SerializeField, Range(0.001f, 0.1f)] private float _speedSit = 0.005f;
    [SerializeField, Range(0.001f, 0.1f)] private float _speedUse = 0.007f;
    
    [Header("Force")]
    [SerializeField, Range(0.01f, 1f)] private float _forceJump = 0.1f;

    [Header("Expenses stamina")]
    [SerializeField, Range(0.1f, 2f)] private float _jumpStamina = 1f;

    private float _currentSpeed = 0;
    private EssenceState _essenceState = EssenceState.None;

    public void Initialization(CharacterController characterController)
    {
        _groundCheck.OnChangeGrounded += ChangeGroundHandler;
        _stamina.OnChangeStamina += (value) => OnChangeStamina?.Invoke(value);
        _currentSpeed = _speedNormal;
        _gravity.Initialization(characterController);
        _characterController = characterController;
        _stamina.SetMaxStamina();
    }

    public void UpdateHandler()
    {
        var isRun = _essenceState == EssenceState.Run;
        _stamina.Update(isRun);
        _gravity.UpdateHandler();
        if (isRun && !_stamina.IsStamina)
        {
            OnChangeRun?.Invoke(false);
        }
    }

    public void MoveInputHandler(Vector2 vector)
    {
        var move = transform.right * vector.x + transform.forward * vector.y;
        _characterController.Move(move * _currentSpeed);
    }

    public void EssenceStateHandler(EssenceState state)
    {
        _essenceState = state;
        _currentSpeed = GetSpeed(state);
        if (state == EssenceState.Run && _stamina.IsCanUseStamina)
        {
            OnChangeRun?.Invoke(true);
        }
    }

    public void Jump()
    {
        if (!_groundCheck.IsGrounded)
        {
            return;
        }

        if (!_stamina.RemoveStamina(_jumpStamina))
        {
            return;
        }

        _gravity.SetVelocity(_forceJump);
    }

    private float GetSpeed(EssenceState state)
    {
        switch (state)
        {
            case EssenceState.Sit:
                return _speedSit;
            case EssenceState.Run:
                return _speedRun;
            case EssenceState.Use:
                return _speedUse;
            default:
                return _speedNormal;
        }
    }

    private void ChangeGroundHandler(bool isGround)
    {
        OnChangeFly?.Invoke(!isGround);
    }
}
