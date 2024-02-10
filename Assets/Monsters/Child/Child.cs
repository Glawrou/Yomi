using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Child : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _speed;

    private const float Height = 300;

    public void Out()
    {
        _audioSource.Play();
        StartCoroutine(OutProcess());
    }

    private IEnumerator OutProcess()
    {
        while (transform.position.y < Height)
        {
            transform.position += Vector3.up * _speed * Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}
