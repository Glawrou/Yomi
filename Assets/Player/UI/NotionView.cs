using UnityEngine;
using UnityEngine.UI;

public class NotionView : MonoBehaviour
{
    [SerializeField] private Text _textCount;

    private string _currentValue;

    public void SetView(bool isView)
    {
        _textCount.text = isView ? _currentValue : string.Empty;
    }

    public void SetCount(int collect, int max)
    {
        _currentValue = $"{collect}/{max}";
        _textCount.text = _currentValue;
    }
}
