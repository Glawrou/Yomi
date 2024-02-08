using System;
using UnityEngine;

public class PlayerInputPc : ControlInput
{
    public override event Action<Vector2> OnRotate;
    public override event Action<Vector2> OnMove;
    public override event Action<CharacterAction, bool> OnCharacterAction;

    [SerializeField] private KeyCode _jump;
    [SerializeField] private KeyCode _sit;
    [SerializeField] private KeyCode _use;
    [SerializeField] private KeyCode _run;

    [Space]
    [SerializeField] private int _throwMouseButton;

    [SerializeField, Range(1f, 1000f)] private float _sensitivity = 50;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        MouseInput();
        MoveInput();
        KeyInput();
    }

    private void MouseInput()
    {
        var rotateY = GetAxis("Mouse Y");
        var rotateX = GetAxis("Mouse X");
        OnRotate?.Invoke(new Vector2(rotateX, rotateY) * _sensitivity);
    }

    private void MoveInput()
    {
        var moveY = GetAxis("Vertical");
        var moveX = GetAxis("Horizontal");
        OnMove?.Invoke(new Vector2(moveX, moveY).normalized);
    }

    private void KeyInput()
    {
        CheckKeyCode(_jump, CharacterAction.Jump);
        CheckKeyCode(_sit, CharacterAction.Sit);
        CheckKeyCode(_use, CharacterAction.Use);
        CheckKeyCode(_run, CharacterAction.Run);
        CheckMouseButton(_throwMouseButton, CharacterAction.Throw);
    }

    private float GetAxis(string key)
    {
        return Input.GetAxis(key) * Time.deltaTime;
    }

    private void CheckKeyCode(KeyCode keyCode, CharacterAction action)
    {
        if (Input.GetKeyDown(keyCode))
        {
            OnCharacterAction?.Invoke(action, true);
        }
        else if (Input.GetKeyUp(keyCode))
        {
            OnCharacterAction?.Invoke(action, false);
        }
    }

    private void CheckMouseButton(int button, CharacterAction action)
    {
        if (Input.GetMouseButtonDown(button))
        {
            OnCharacterAction?.Invoke(action, true);
        }
        else if (Input.GetMouseButtonUp(button))
        {
            OnCharacterAction?.Invoke(action, false);
        }
    }
}
