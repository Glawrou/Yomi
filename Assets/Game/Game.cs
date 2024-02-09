using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private MenuWindow _menuWindow;
    [SerializeField] private PlayerInputPc _playerInput;

    private void Start()
    {
        _menuWindow.OnOpen += PauseGame;
        _menuWindow.OnClose += ResumeGame;
        _menuWindow.OnExitGame += GameExit;
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
}
