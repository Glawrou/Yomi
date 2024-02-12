using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flashlight : DraggableObject
{
    [SerializeField] private Text _text;
    [SerializeField] private DeadEffect _deadEffect;

    private const int DistanceToDie = 1000;

    private float _distance = 0;

    private void Update()
    {
        if (DistanceToDie < _distance)
        {
            Instantiate(_deadEffect, transform.position, Quaternion.identity, null);
            Destroy(gameObject);
            return;
        }

        _text.text = (DistanceToDie - (int)_distance).ToString();
        _distance += Vector3.Distance(_rigidbody.velocity, Vector3.zero);
    }
}
