using System.Collections;
using UnityEngine;

public class Level2 : Game
{
    [SerializeField] private Child[] _childPrefab;
    [SerializeField] private float DistancePlayerSpawn = 20f;

    private const float TimeBetweenSpawns = 10f;
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
            _currentChild = Instantiate(
                _childPrefab[Random.Range(0, _childPrefab.Length)],
                _player.transform.forward * -DistancePlayerSpawn,
                Quaternion.identity,
                null);
            _currentChild.Initialization(_player);
        }
    }
}
