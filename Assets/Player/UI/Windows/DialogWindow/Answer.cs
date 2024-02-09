using System;
using UnityEngine;
using UnityEngine.UI;

public class Answer : MonoBehaviour
{
    public event Action<DialogData> OnPress;

    [SerializeField] private Text _text;
    [SerializeField] private Button _button;

    private DialogData _dialogData;

    private void Awake()
    {
        _button.onClick.AddListener(() => OnPress?.Invoke(_dialogData));
    }

    public void Open(DialogData dialogData)
    {
        _dialogData = dialogData;
        _text.text = dialogData.Answer;
    }
}
