using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Essence : MonoBehaviour
{
    [SerializeField] private ControlInput _controlInput;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private EssenceRotate _rotate;
    [SerializeField] private EssenceMovement _movement;

    private void Awake()
    {
        _controlInput.OnCharacterAction += CharacterActionHandler;
    }

    private void Start()
    {
        InitRotate();
        InitMovement();
    }

    private void InitRotate()
    {
        _controlInput.OnRotate += _rotate.MouseInputHandler;
    }

    private void InitMovement()
    {
        _movement.Initialization(_characterController);
        _controlInput.OnMove += _movement.MoveInputHandler;
    }

    private void Update()
    {
        _movement.UpdateHandler();
    }

    private void CharacterActionHandler(CharacterAction action, bool isActive)
    {
        switch (action)
        {
            case CharacterAction.Jump:
                JumpHandler(isActive);
                break;
            case CharacterAction.Sit:
                break;
            case CharacterAction.Use:
                break;
            case CharacterAction.Run:
                _movement.SetRun(isActive);
                break;
            default:
                break;
        }
    }

    private void JumpHandler(bool isActive)
    {
        if (!isActive)
        {
            return;
        }

        _movement.Jump();
    }
}
