using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaView : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField, Range(0f, 1f)] private float _factor;

    public void SetValue(float value)
    {
        value = (1 - Mathf.Clamp(value, 0, 1)) * _factor;
        _image.color = new Color(
            _image.color.r, 
            _image.color.g, 
            _image.color.b,
            value);
    }
}
