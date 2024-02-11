using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : Game
{
    [SerializeField] private Sociable—haracter _izanami;

    private new void Awake()
    {
        base.Awake();
        _izanami.OnEndDialog += StartLevel;
    }

    private new void Start()
    {
        base.Start();
        _playerUI.SetNotionView(false);
        SetViewNotions(false);
    }

    private void StartLevel()
    {
        _playerUI.SetNotionView(true);
        SetViewNotions(true);
    }

    private void SetViewNotions(bool isActive)
    {
        foreach (var item in _collectibleObjects)
        {
            item.gameObject.SetActive(isActive);
        }
    }
}
