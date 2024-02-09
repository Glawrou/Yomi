using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DialogWindow : Window
{
    public event Action OnEndDialog;

    [SerializeField] private TextView _textView;
    [SerializeField] private ButtonsAnswer _buttonsAnswer;
    [SerializeField] private DialogData _dialogData;
    [SerializeField] private AudioSource _audioSource;

    private new void Awake()
    {
        base.Awake();
        _buttonsAnswer.OnSelect += (data) => StartCoroutine(Print(data));
    }

    public void Open(DialogData dialogData)
    {
        SetWindow(true);
        SetCursor(true);
        StartCoroutine(Print(dialogData));
    }

    public override void Close()
    {
        SetWindow(false);
        SetCursor(false);
    }

    private IEnumerator Print(DialogData dialogData)
    {
        if (dialogData.Text == string.Empty)
        {
            Close();
            OnEndDialog?.Invoke();
            yield break;
        }

        _buttonsAnswer.Clear();
        Sey(dialogData.AudioText);
        _textView.Print(dialogData.Text, dialogData.AudioText.length);
        yield return new WaitForSeconds(dialogData.AudioText.length);
        _buttonsAnswer.Open(dialogData.Answers);
    }

    private void Sey(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
    }
}
