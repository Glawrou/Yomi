using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [SerializeField] private Elevator _elevatorPrifab;
    [SerializeField] private Player _playerPrifab;
    [SerializeField] private string _nextLevel;

    [Space]
    [SerializeField] protected List<CollectibleObject> _collectibleObjects;

    private PlayerInputPc _playerInput;
    protected PlayerUI _playerUI;
    private Player _player;
    private MenuWindow _menuWindow => _playerUI.MenuWindow;

    private const int MaxCollect = 5;
    private const int HeightSpawnPlayer = 5;
    private const int HeightSpawnElevator = 50;

    protected void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        foreach (var item in _collectibleObjects)
        {
            item.OnCollect += CollectHandler;
        }
    }

    protected void Start()
    {
        InitPlayer();
        _menuWindow.OnOpen += PauseGame;
        _menuWindow.OnClose += ResumeGame;
        _menuWindow.OnExitGame += GameExit;
    }

    private void InitPlayer()
    {
        _player = SpawnPlayer();
        _playerInput = _player.ControlInput as PlayerInputPc;
        _playerUI = _player.PlayerUI;
    }

    private Player SpawnPlayer()
    {
        return SpawnElevator(_playerPrifab, Vector3.up * HeightSpawnPlayer);
    }

    private Player SpawnElevator(Player player, Vector3 pos)
    {
        var elevator = Instantiate(_elevatorPrifab, pos, Quaternion.identity, null);
        return elevator.Spawn(player);
    }

    private Elevator SpawnElevator(Vector3 pos)
    {
        var elevator = Instantiate(_elevatorPrifab, pos, Quaternion.identity, null);
        elevator.Spawn(null);
        return elevator;
    }

    private void SetSensitivity(float value)
    {
        _playerInput.Sensitivity = value;
    }

    private void GameExit()
    {
        Application.Quit();
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
    }

    private void ResumeGame()
    {
        Time.timeScale = 1;
    }

    private void CollectHandler(CollectibleObject collectibleObject)
    {
        if (_collectibleObjects.Count == 1)
        {
            SpawnElevator(_player.transform.position + Vector3.up * HeightSpawnElevator).OnCloseDoor += LoadNextLevel;
        }

        _collectibleObjects.Remove(collectibleObject);
        _playerUI.SetNotionCollect(MaxCollect - _collectibleObjects.Count, MaxCollect);
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(_nextLevel);
    }
}
