using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4 : Game
{
    [SerializeField] private Spider _spider;

    private new void Start()
    {
        base.Start();
        _spider.Initialization(_player);
    }
}
