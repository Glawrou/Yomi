using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private StaminaView _staminaView;

    [field: SerializeField] public MenuWindow MenuWindow { get; private set; }
    [field: SerializeField] public DialogWindow DialogWindow { get; private set; }

    public void SetStamina(float value) => _staminaView.SetValue(value);
}
