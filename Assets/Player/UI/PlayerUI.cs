using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private StaminaView _staminaView;

    public void SetStamina(float value) => _staminaView.SetValue(value);
}
