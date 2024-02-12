using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level5 : Game
{
    [SerializeField] private Sociable—haracter _izanami;

    private new void Start()
    {
        base.Start();
        _izanami.OnEndDialog += LoadNextLevel;
    }
}
