using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class Background : MonoBehaviour, IPointerClickHandler
{
    public event Action OnClick;

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClick?.Invoke();
    }
}
