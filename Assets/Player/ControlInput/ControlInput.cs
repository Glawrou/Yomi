using System;
using UnityEngine;

public abstract class ControlInput : MonoBehaviour
{
    public abstract event Action<Vector2> OnRotate;
    public abstract event Action<Vector2> OnMove;
    public abstract event Action<CharacterAction, bool> OnCharacterAction;
}
