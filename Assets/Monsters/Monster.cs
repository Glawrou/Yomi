using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Monster : MonoBehaviour
{
    public event Action OnDead;

    [SerializeField] protected EssenceMovement _essenceMovement;
    [SerializeField] protected EssenceGravity _essenceGravity;

    [SerializeField] private CharacterController _characterController;
    [SerializeField] private ColliderTrigger _colliderTrigger;
    [SerializeField] private float _offsetRotate = 0f;
    [SerializeField] private float _speedMove = 50f;

    public abstract void Detected();

    private Player _player;

    private void Start()
    {
        _essenceMovement.Initialization(_characterController);
        _colliderTrigger.OnEnterTrigger += KillPlayer;
    }

    public void Initialization(Player player)
    {
        _player = player;
    }    

    public void Update()
    {
        _essenceMovement.UpdateHandler();
        if (!_player)
        {
            return;
        }

        SetRotateY(_player.transform.position);
        _essenceMovement.MoveInputHandler(-Vector2.right * _speedMove);
    }

    private void SetRotateY(Vector3 targetCoordinates)
    {
        var direction = (targetCoordinates - transform.position).normalized;
        var targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        var targetRotation = Quaternion.Euler(0f, targetAngle + _offsetRotate, 0f);
        transform.rotation = targetRotation;
    }

    protected void Dead()
    {
        OnDead?.Invoke();
        Destroy(gameObject);
    }

    private void KillPlayer(GameObject obj)
    {
        if (obj.tag != Player.PlayerTag)
        {
            return;
        }

        _player.Kill();
    }
}
