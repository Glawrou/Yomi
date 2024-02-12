using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Monster
{
    [SerializeField] private Animator _animator;

    private const string AnimTrigger = "Velosity";

    private StateSpider _stateSpide = StateSpider.Approximation;

    private void Update()
    {
        switch (_stateSpide)
        {
            case StateSpider.Approximation:
                _animator.SetFloat(AnimTrigger, 1);
                Move(-Vector2.right);
                break;
            case StateSpider.Escape:
                _animator.SetFloat(AnimTrigger, -1);
                break;
            default:
                break;
        }
    }

    public override void Detected()
    {
        _stateSpide = StateSpider.Escape;
    }
}
