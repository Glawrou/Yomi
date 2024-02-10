using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ColliderTrigger : MonoBehaviour
{
    public event Action<GameObject> OnEnterTrigger;
    public event Action<GameObject> OnExitTrigger;

    private void OnTriggerEnter(Collider other)
    {
        OnEnterTrigger?.Invoke(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        OnExitTrigger?.Invoke(other.gameObject);
    }
}
