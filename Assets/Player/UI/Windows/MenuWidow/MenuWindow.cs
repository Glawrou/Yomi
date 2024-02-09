using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MenuWindow : Window
{
    public event Action OnExitGame;

    [Header("Buttons")]
    [SerializeField] private Button _play;
    [SerializeField] private Button _exit;

    private new void Awake()
    {
        base.Awake();
        _play.onClick.AddListener(() => Close());
        _exit.onClick.AddListener(() => OnExitGame?.Invoke());
    }

    public void Open()
    {
        SetWindow(true);
        SetCursor(true);
    }

    public override void Close()
    {
        SetWindow(false);
        SetCursor(false);
    }
}
