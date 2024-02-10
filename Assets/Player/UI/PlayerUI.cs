using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private StaminaView _staminaView;
    [SerializeField] private NotionView _notionView;

    [field: SerializeField] public MenuWindow MenuWindow { get; private set; }
    [field: SerializeField] public DialogWindow DialogWindow { get; private set; }

    public void SetStamina(float value) => _staminaView.SetValue(value);

    public void SetNotionCollect(int collect, int maxCollect) => _notionView.SetCount(collect, maxCollect);
    public void SetNotionView(bool isView) => _notionView.SetView(isView);
}
