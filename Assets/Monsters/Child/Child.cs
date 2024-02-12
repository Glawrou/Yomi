using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Child : Monster
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _speedOut = 5;

    private const float Height = 300;

    private void Out()
    {
        _audioSource.Play();
        _essenceGravity.SetUseGravity(false);
        StartCoroutine(OutProcess());
    }

    public void Update()
    {
        Move(-Vector2.right);
    }

    private IEnumerator OutProcess()
    {
        while (transform.position.y < Height)
        {
            transform.position += Vector3.up * _speedOut * Time.deltaTime;
            yield return null;
        }

        Dead();
    }

    public override void Detected()
    {
        Out();
    }
}
