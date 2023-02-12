using NTC.Global.Cache;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationController : MonoCache
{
    private CollectorController[] _collectorControllers;
    private Animator _animator;
    protected override void OnEnabled()
    {
        for (int i = 0; i < _collectorControllers.Length; i++)
        {
            _collectorControllers[i].OnWrong += OnWrongNoteCollected;
        }
    }
    protected override void OnDisabled()
    {
        for (int i = 0; i < _collectorControllers.Length; i++)
        {
            _collectorControllers[i].OnWrong -= OnWrongNoteCollected;
        }
    }
    private void Awake()
    {
        _collectorControllers = FindObjectsOfType<CollectorController>();
        _animator = Get<Animator>();
    }
    private void OnWrongNoteCollected()
    {
        _animator.Play("Wrong");
    }

}
