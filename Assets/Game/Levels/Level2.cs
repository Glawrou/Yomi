using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2 : Game
{
    [SerializeField] private Child _childPrefab;

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
            yield return new WaitForSeconds(25f);
            if (_currentChild)
            {
                continue;
            }

            _currentChild = Instantiate(_childPrefab, _player.transform.forward * -20f, Quaternion.identity, null);
            _currentChild.Initialization(_player);
            _currentChild.OnDead += () => _currentChild = null;
        }
    }
}
