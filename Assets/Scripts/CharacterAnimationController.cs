using NTC.Global.Cache;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationController : MonoCache
{
    //private CollectorController[] _collectorControllers;

    private Animator _animator;
    protected override void OnEnabled()
    {/*
        if (Screen.autorotateToPortrait != true)
        {
            _animator.SetBool("IsGallery", false);
            ControllersHandler.OnAnyMissed += OnWrongNoteCollected;
        }
        else
        {
            _animator.SetBool("IsGallery", true);
        }*/
        ControllersHandler.OnAnyMissed += OnWrongNoteCollected;
        Spawner.OnFirstNote += OnGameStarted;
        Spawner.OnLastNote += ChangeToIdle;
    }
    protected override void OnDisabled()
    {
        // if (Screen.autorotateToPortrait != true)
        // {
        ControllersHandler.OnAnyMissed -= OnWrongNoteCollected;
        Spawner.OnFirstNote -= OnGameStarted;
        Spawner.OnLastNote -= ChangeToIdle;

        // }
    }
    private void Awake()
    {
        _animator = Get<Animator>();
    }

    private void OnWrongNoteCollected()
    {
        _animator.Play("Wrong");
    }

    private void OnGameStarted()
    {
        _animator.SetBool("IsGameStarted", true);
    }

    public void ChangeToIdle()
    {
        _animator.SetTrigger("Idle");
    }
}
