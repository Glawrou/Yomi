using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TextView : MonoBehaviour
{
    [SerializeField] private Text _textField;
    [SerializeField, Range(0f, 1f)] private float _speed = 0.2f;

    private Coroutine _printProcess;

    public void Print(string text, float seconds)
    {
        if (_printProcess != null)
        {
            StopCoroutine(_printProcess);
            _printProcess = null;
        }

        _printProcess = StartCoroutine(PrintProcess(text, seconds));
    }

    private IEnumerator PrintProcess(string text, float seconds)
    {
        _textField.text = string.Empty;
        foreach (var c in text)
        {
            _textField.text += c;
            yield return new WaitForSeconds(seconds / text.Length);
        }
    }
}
