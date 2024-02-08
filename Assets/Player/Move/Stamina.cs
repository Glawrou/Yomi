using System;
using UnityEngine;

[Serializable]
public class Stamina
{
    public event Action<float> OnChangeStamina;

    public float Value => _stamina / MaxStamina;
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
            SetStamina(stamina);
            return true;
        }

        return false;
    }

    public void SetMaxStamina()
    {
        SetStamina(MaxStamina);
    }

    public void Update(bool isUse)
    {
        SetStamina(_stamina + (isUse ? -Time.deltaTime : Time.deltaTime * AddStaminaFactor));
    }

    private void SetStamina(float value)
    {
        _stamina = Mathf.Clamp(value, 0, MaxStamina);
        OnChangeStamina?.Invoke(Value);
    }
}