using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private CollectorController[] _collectorControllers;

    [SerializeField] private string[] _animationNames;
    private void OnEnable()
    {
        for (int i = 0; i < _collectorControllers.Length; i++)
        {
            _collectorControllers[i].OnWrong += OnWrongNoteCollected;
        }
    }
    private void OnDisable()
    {
        for (int i = 0; i < _collectorControllers.Length; i++)
        {
            _collectorControllers[i].OnWrong -= OnWrongNoteCollected;
        }
    }
    private void Awake()
    {
        _collectorControllers = FindObjectsOfType<CollectorController>();
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
