using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Essence : MonoBehaviour
{
    public event Action<EssenceState> OnChangeState;

    [SerializeField] private ControlInput _controlInput;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private LookRotate _rotate;
    [SerializeField] private EssenceMovement _movement;
    [SerializeField] private Hands _hands;

    private EssenceState _essenceState;

    private void Awake()
    {
        _controlInput.OnCharacterAction += CharacterActionHandler;
    }

    private void Start()
    {
        InitRotate();
        InitMovement();
        InitHends();
    }

    private void InitRotate()
    {
        OnChangeState += (state) => _rotate.SetSit(state == EssenceState.Sit);
        _controlInput.OnRotate += _rotate.MouseInputHandler;
    }

    private void InitMovement()
    {
        OnChangeState += _movement.EssenceStateHandler;
        _movement.OnChangeFly += (isFly) => SetState(isFly ? EssenceState.Fly : EssenceState.None);
        _movement.Initialization(_characterController);
        _controlInput.OnMove += _movement.MoveInputHandler;
    }

    private void InitHends()
    {
        _hands.OnChangeUse += (active) => SetState(active ? EssenceState.Use : EssenceState.None);
    }

    private void Update()
    {
        _movement.UpdateHandler();
    }

    private void SetState(EssenceState state)
    {
        if (_essenceState != EssenceState.Use)
        {
            _hands.Drop();
        }

        _essenceState = state;
        OnChangeState?.Invoke(state);
    }

    private void JumpHandler(bool isActive)
    {
        if (!isActive)
        {
            return;
        }

        _movement.Jump();
    }

    private void SitHandler(bool isActive)
    {
        if (!isActive)
        {
            return;
        }

        if (_essenceState == EssenceState.Sit)
        {
            SetState(EssenceState.None);
            return;
        }

        if (_essenceState != EssenceState.None)
        {
            return;
        }

        SetState(EssenceState.Sit);
    }

    private void UseHandler(bool isActive)
    {
        if (!isActive)
        {
            return;
        }

        if (_rotate.Hit == null || !_rotate.Hit.gameObject.TryGetComponent<SelectableObject>(out var obj))
        {
            return;
        }

        _hands.Use(obj);
    }

    private void RunHandler(bool isActive)
    {
        if (_essenceState == EssenceState.Fly || _essenceState == EssenceState.Use)
        {
            return;
        }

        if (!isActive && _essenceState != EssenceState.Run)
        {
            return;
        }

        SetState(_essenceState == EssenceState.Run ? EssenceState.None : EssenceState.Run);
    }

    private void ThrowHandler(bool isActive)
    {
        if (!isActive)
        {
            return;
        }

        _hands.Throw();
    }

    private void CharacterActionHandler(CharacterAction action, bool isActive)
    {
        switch (action)
        {
            case CharacterAction.Jump:
                JumpHandler(isActive);
                break;
            case CharacterAction.Sit:
                SitHandler(isActive);
                break;
            case CharacterAction.Use:
                UseHandler(isActive);
                break;
            case CharacterAction.Run:
                RunHandler(isActive);
                break;
            case CharacterAction.Throw:
                ThrowHandler(isActive);
                break;
            default:
                break;
        }
    }
}
