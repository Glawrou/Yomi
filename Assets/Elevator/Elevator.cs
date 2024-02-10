using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Elevator : MonoBehaviour
{
    public event Action OnElevatorOut;
    public event Action OnPlayerOut;
    public event Action OnCloseDoor;
    public event Action OnPlayerEnter;
    public event Action OnPlayerExit;

    [SerializeField] private Animator _animator;
    [SerializeField] private ColliderTrigger _triggerPlayerIn;
    [SerializeField] private ColliderTrigger _triggerPlayerUnder;
    [SerializeField] private ColliderTrigger _triggerPlayerOut;
    [SerializeField] private GroundCheck _groundCheck;
    [SerializeField] private AudioSource _bell;
    [SerializeField] private float _speed = 1;

    private const string AnimTrigger = "IsOpen";
    private const string PlayerTag = "Player";
    private const float BisappearanceHeight = 100;
    private const float DoorAnimSeconds = 1.5f;

    private bool IsPlayerInElevator => _player;
    private bool IsPlayerUnderElevator = false;
    private Player _player;

    private void Awake()
    {
        _triggerPlayerIn.OnEnterTrigger += ElevatorEnterHandler;
        _triggerPlayerIn.OnExitTrigger += ElevatorExitHandler;
        _triggerPlayerUnder.OnEnterTrigger += UnderElevatorEnterHandler;
        _triggerPlayerUnder.OnExitTrigger += UnderElevatorExitHandler;
        _triggerPlayerOut.OnExitTrigger += (obj) => OnPlayerOut?.Invoke();
    }

    public Player Spawn(Player player)
    {
        if (player)
        {
            OnPlayerOut += () => StartCoroutine(OutProcess());
        }
        else
        {
            OnPlayerEnter += () => StartCoroutine(OutProcess());
        }

        StartCoroutine(SpawnProcess(player));
        if (player == null)
        {
            return null;
        }

        return Instantiate(player, transform.position, Quaternion.identity, transform);
    }

    private IEnumerator SpawnProcess(Player player)
    {
        var isGround = false;
        _groundCheck.OnChangeGrounded += (ground) => isGround = ground;
        while (!isGround)
        {
            yield return new WaitWhile(() => IsPlayerUnderElevator);
            transform.position += Vector3.up * -_speed * Time.deltaTime;
            yield return null;
        }

        OpenDoor();
    }

    private IEnumerator OutProcess()
    {
        CloseDoor();
        yield return new WaitForSeconds(DoorAnimSeconds);
        while (transform.position.y < BisappearanceHeight)
        {
            transform.position += Vector3.up * _speed * Time.deltaTime;
            yield return null;
        }

        OnElevatorOut?.Invoke();
        if (!IsPlayerInElevator)
        {
            Destroy(gameObject);
        }
    }

    [ContextMenu("Open")]
    public void OpenDoor()
    {
        _animator.SetBool(AnimTrigger, true);
    }

    [ContextMenu("Close")]
    public void CloseDoor()
    {
        _animator.SetBool(AnimTrigger, false);
    }

    public void ElevatorEnterHandler(GameObject obj)
    {
        if (obj.tag == PlayerTag)
        {
            OnPlayerEnter?.Invoke();
            _player = obj.GetComponent<Player>();
            obj.transform.parent = transform;
        }
    }

    public void ElevatorExitHandler(GameObject obj)
    {
        if (obj.tag == PlayerTag)
        {
            OnPlayerExit?.Invoke();
            _player = null;
            obj.transform.parent = null;
        }
    }

    public void UnderElevatorEnterHandler(GameObject obj)
    {
        if (obj.tag == PlayerTag)
        {
            IsPlayerUnderElevator = true;
        }
    }

    public void UnderElevatorExitHandler(GameObject obj)
    {
        if (obj.tag == PlayerTag)
        {
            IsPlayerUnderElevator = false;
        }
    }

    public void CloseDoorHandler()
    {
        OnCloseDoor?.Invoke();
    }

    public void OpenDoorHandler()
    {
        _bell.Play();
    }
}
