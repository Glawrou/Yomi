using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ButtonsAnswer : MonoBehaviour
{
    public event Action<DialogData> OnSelect;

    [SerializeField] private Answer _answerPrefab;
    [SerializeField] private DialogData _endDialog;

    private List<Answer> _answers = new List<Answer>();

    public void Open(DialogData[] dialogDatas)
    {
        if (dialogDatas.Length == 0)
        {
            Clear();
            CriateEnd();
            return;
        }

        if (_answers.Count != 0)
        {
            Clear();
        }

        Criate(dialogDatas);
    }

    public void Clear()
    {
        foreach (var item in _answers)
        {
            item.OnPress -= PressHandler;
            Destroy(item.gameObject);
        }

        _answers.Clear();
    }

    private void Criate(DialogData[] dialogDatas)
    {
        foreach (var item in dialogDatas)
        {
            Criare(item).OnPress += PressHandler;
        }
    }

    private Answer Criare(DialogData dialogDatas)
    {
        var ans = Instantiate(_answerPrefab, transform);
        ans.Open(dialogDatas);
        _answers.Add(ans);
        return ans;
    }

    private void CriateEnd()
    {
        Criare(_endDialog).OnPress += PressHandler;
    }

    private void PressHandler(DialogData dialogData)
    {
        OnSelect?.Invoke(dialogData);
    }
}
