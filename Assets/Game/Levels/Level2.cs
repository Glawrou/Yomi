using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2 : Game
{
    [SerializeField] private Child _childPrefab;

    private const float DistancePlayerSpawn = 20f;
    private const float TimeBetweenSpawns =25f;
    private Child _currentChild;

    private new void Start()
    {
        base.Start();
        StartCoroutine(SpawnChild());
    }

    private IEnumerator SpawnChild()
    {
        while (true)
        {
            yield return new WaitForSeconds(TimeBetweenSpawns);
            _currentChild = Instantiate(_childPrefab, _player.transform.forward * -DistancePlayerSpawn, Quaternion.identity, null);
            _currentChild.Initialization(_player);
        }
    }
}
