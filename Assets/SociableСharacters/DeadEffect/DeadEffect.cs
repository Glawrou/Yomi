using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadEffect : MonoBehaviour
{
    [SerializeField] private float Seconds = 5;

    private void Start()
    {
        StartCoroutine(WaitAndDead());
    }

    private IEnumerator WaitAndDead()
    {
        yield return new WaitForSeconds(Seconds);
        Destroy(gameObject);
    }
}
