using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MenuWindow : Window
{
    public event Action<float> OnSensitivity;
    public event Action<float> OnSound;
    public event Action<float> OnMusic;
    public event Action<float> OnBrightness;
    public event Action OnExitGame;

    [Header("Buttons")]
    [SerializeField] private Button _play;
    [SerializeField] private Button _exit;

    [Header("Scrollbars")]
    [SerializeField] private Slider _sensitivity;
    [SerializeField] private Slider _sound;
    [SerializeField] private Slider _music;

    private new void Awake()
    {
        base.Awake();
        _play.onClick.AddListener(() => Close());
        _exit.onClick.AddListener(() => OnExitGame?.Invoke());
    }

    private void Start()
    {
        _sensitivity.onValueChanged.AddListener((value) => OnSensitivity?.Invoke(value));
        _sound.onValueChanged.AddListener((value) => OnSound?.Invoke(value));
        _music.onValueChanged.AddListener((value) => OnMusic?.Invoke(value));
    }

    public void SetSensitivity(float value)
    {
        _sensitivity.value = value;
    }

    public void SetSound(float value)
    {
        _sound.value = 1 - value;
    }

    public void SetMusic(float value)
    {
        _music.value = 1 - value;
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
