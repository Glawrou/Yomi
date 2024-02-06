using System;
using UnityEngine;

[Serializable]
public class Stamina
{
    public float Value => _stamina;
    public bool IsCanUseStamina => MinStaminaForUse < _stamina;
    public bool IsStamina => _stamina > 0;

    [field: SerializeField] public float MaxStamina { get; private set; }
    [field: SerializeField] public float MinStaminaForUse { get; private set; }
    [field: SerializeField] public float AddStaminaFactor { get; private set; }

    private float _stamina;

    public bool RemoveStamina(float value)
    {
        var stamina = _stamina - value;
        if (stamina > 0)
        {
            _stamina = stamina;
            return true;
        }

        return false;
    }

    public void SetMaxStamina()
    {
        _stamina = MaxStamina;
    }

    public void Update(bool isUse)
    {
        _stamina += isUse ? -Time.deltaTime : Time.deltaTime * AddStaminaFactor;
        _stamina = Mathf.Clamp(_stamina, 0, MaxStamina);
    }
}