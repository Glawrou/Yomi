using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SociableСharacter : MonoBehaviour
{
    [field: SerializeField] public DialogData DialogData { get; private set; }
    [field: SerializeField] public Transform Head { get; private set; }

    [SerializeField] public DeadEffect _deadEffect;
    [SerializeField] private ColliderTrigger _colliderTrigger;
    [SerializeField] private LockAt _lockAt;
    
    private const string PlayerTag = "Player";

    private void Awake()
    {
        _colliderTrigger.OnEnterTrigger += TriggerHandler;
    }

    public void Dead()
    {
        Instantiate(_deadEffect, transform.position, Quaternion.identity, null);
        Destroy(gameObject);
    }

    private void TriggerHandler(GameObject player)
    {
        if (PlayerTag == player.tag)
        {
            _lockAt.SetTarget(player.transform);
            var pla = player.GetComponent<Player>();
            pla.StartDialog(this);
        }
    }
}
