using System;
using UnityEngine;

public abstract class Window : MonoBehaviour
{
    public event Action OnOpen;
    public event Action OnClose;

    public bool IsOpen => _openedWindow == this;

    [SerializeField] private Background _background;
    [SerializeField] private ViewWindow _viewWindow;

    protected static Window _openedWindow;

    protected void Awake()
    {
        _background.OnClick += Close;
    }

    public abstract void Close();

    protected void SetCursor(bool isActive)
    {
        if (!isActive)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    protected void SetWindow(bool isActive)
    {
        if (isActive && _openedWindow != null)
        {
            return;
        }

        if (!isActive && _openedWindow != this)
        {
            return;
        }

        _viewWindow.Set(isActive);
        if (isActive)
        {
            _openedWindow = this;
            PlayerInputPc.IsActive = false;
            OnOpen?.Invoke();
        }
        else
        {
            _openedWindow = null;
            PlayerInputPc.IsActive = true;
            OnClose?.Invoke();
        }
    }
}
