using NTC.Global.Cache;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationController : MonoCache
{
    //private CollectorController[] _collectorControllers;

    private Animator _animator;
    protected override void OnEnabled()
    {
        if (Screen.autorotateToPortrait != true)
        {
            _animator.SetBool("IsGallery", false);
            ControllersHandler.OnAnyMissed += OnWrongNoteCollected;
        }
        else
        {
            _animator.SetBool("IsGallery", true);
        }
    }
    protected override void OnDisabled()
    {
        if (Screen.autorotateToPortrait != true)
        {
            ControllersHandler.OnAnyMissed -= OnWrongNoteCollected;
        }
    }
    private void Awake()
    {
        _animator = Get<Animator>();
    }

    private void OnWrongNoteCollected()
    {
        _animator.Play("Wrong");
    }
}
