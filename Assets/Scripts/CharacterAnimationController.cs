using NTC.Global.Cache;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationController : MonoCache
{

    [SerializeField] private string[] _animationNames;
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
    private void Start()
    {
        StartCoroutine(PlayNewAnimation());

    }

    private void OnWrongNoteCollected()
    {
        _animator.Play("Wrong");
        StartCoroutine(PlayNewAnimation());
    }

    private IEnumerator PlayNewAnimation()
    {
        yield return new WaitForSeconds(3);
        _animator.Play(_animationNames[Random.Range(0, _animationNames.Length)]);
    }
}
