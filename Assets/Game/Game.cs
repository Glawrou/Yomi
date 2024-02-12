using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class Game : MonoBehaviour
{
    [SerializeField] private Elevator _elevatorPrifab;
    [SerializeField] private Player _playerPrifab;
    [SerializeField] private string _nextLevel;
    [SerializeField] private AudioMixer _mixer;

    [Space]
    [SerializeField] protected List<CollectibleObject> _collectibleObjects;

    private PlayerInputPc _playerInput;
    protected PlayerUI _playerUI;
    protected Player _player;
    private MenuWindow _menuWindow => _playerUI.MenuWindow;

    private const int MaxCollect = 5;
    private const int HeightSpawnPlayer = 5;
    private const int HeightSpawnElevator = 50;
    private const string DeadScene = "Dead";
    private const float SoundValue = -80f;

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
        InitMenu();
        _menuWindow.OnOpen += PauseGame;
        _menuWindow.OnClose += ResumeGame;
        _menuWindow.OnExitGame += GameExit;
    }

    private void InitPlayer()
    {
        _player = SpawnPlayer();
        _playerInput = _player.ControlInput as PlayerInputPc;
        _playerUI = _player.PlayerUI;
        _player.OnDead += LoadDeadScene;
        _playerUI.Compass.Initialization(_collectibleObjects.ToArray());
    }

    private void InitMenu()
    {
        _menuWindow.SetSensitivity(_playerInput.Sensitivity);
        _mixer.GetFloat("Sound", out var soundValue);
        _menuWindow.SetSound(soundValue);
        _mixer.GetFloat("Music", out var soundMusic);
        _menuWindow.SetMusic(soundMusic);
        _menuWindow.OnSensitivity += SetSensitivity;
        _menuWindow.OnMusic += SetMusic;
        _menuWindow.OnSound += SetSound;
        _menuWindow.OnBrightness += SetBrightness;
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

    private void SetSound(float value)
    {
        _mixer.SetFloat("Sound", (1 - value) * SoundValue);
    }

    private void SetMusic(float value)
    {
        _mixer.SetFloat("Music", (1 - value) * SoundValue);
    }

    private void SetBrightness(float value)
    {
        RenderSettings.skybox.SetFloat("_Exposure", value);
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
            _playerUI.Compass.gameObject.SetActive(false);
        }

        _collectibleObjects.Remove(collectibleObject);
        _playerUI.SetNotionCollect(MaxCollect - _collectibleObjects.Count, MaxCollect);
        _playerUI.Compass.Initialization(_collectibleObjects.ToArray());
    }

    protected void LoadNextLevel()
    {
        SceneManager.LoadScene(_nextLevel);
    }

    private void LoadDeadScene()
    {
        SceneManager.LoadScene(DeadScene);
    }
}
