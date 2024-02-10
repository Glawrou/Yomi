using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public event Action<EssenceState> OnChangeState;

    [field: SerializeField] public ControlInput ControlInput { get; private set; }
    [field: SerializeField] public PlayerUI PlayerUI { get; private set; }

    [Space]
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private LookRotate _rotate;
    [SerializeField] private EssenceMovement _movement;
    [SerializeField] private Hands _hands;

    private EssenceState _essenceState;

    private void Awake()
    {
        ControlInput.OnCharacterAction += CharacterActionHandler;
    }

    private void Start()
    {
        InitRotate();
        InitMovement();
        InitHends();
        InitUI();
    }

    public void SetCollect(int collect, int max) => PlayerUI.SetNotionCollect(collect, max);
    public void SetViewNotion(bool isView) => PlayerUI.SetNotionView(isView);

    public void StartDialog(Sociable�haracter sociable�haracters)
    {
        _rotate.LockAt(sociable�haracters.Head.transform);
        PlayerUI.DialogWindow.Open(sociable�haracters.DialogData);
        PlayerUI.DialogWindow.OnEndDialog += () => EndDialog(sociable�haracters);
    }

    public void EndDialog(Sociable�haracter sociable�haracters)
    {
        sociable�haracters.Dead();
    }

    private void InitRotate()
    {
        OnChangeState += (state) => SetSit(state == EssenceState.Sit);
        ControlInput.OnRotate += _rotate.MouseInputHandler;
    }

    private void InitMovement()
    {
        OnChangeState += _movement.EssenceStateHandler;
        _movement.OnChangeFly += (isFly) => SetState(isFly ? EssenceState.Fly : EssenceState.None);
        _movement.OnChangeRun += (isRun) => SetState(isRun ? EssenceState.Run : EssenceState.None);
        _movement.Initialization(_characterController);
        ControlInput.OnMove += _movement.MoveInputHandler;
    }

    private void InitHends()
    {
        _hands.OnChangeUse += (active) => SetState(active ? EssenceState.Use : EssenceState.None);
    }

    private void InitUI()
    {
        _movement.OnChangeStamina += PlayerUI.SetStamina;
    }

    private void Update()
    {
        _movement.UpdateHandler();
    }

    private void SetSit(bool isSit)
    {
        if (isSit == _rotate.IsSit)
        {
            return;
        }

        _rotate.SetSit(isSit);
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

    private void MenuHandler(bool isActive)
    {
        if (!isActive)
        {
            return;
        }

        var window = PlayerUI.MenuWindow;
        Action action = window.IsOpen ? window.Close : window.Open;
        action?.Invoke();
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
            case CharacterAction.Menu:
                MenuHandler(isActive);
                break;
            default:
                break;
        }
    }
}
