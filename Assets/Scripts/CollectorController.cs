using NTC.Global.Cache;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CollectorController : MonoCache
{
    public event UnityAction OnCollected;
    public event UnityAction OnWrong;

    [SerializeField] private NoteCollector _noteCollector;
    [SerializeField] private DeadZone _deadZone;
    [SerializeField] private Button _button;
    [SerializeField] private Collor _color;
    [SerializeField] private KeyCode _keyCode;
    [SerializeField] private ParticleSystem _effect;
    [SerializeField] private AudioClip _missClip, _hitClip;

    private bool _canBePressed;
    private Note _note;
    protected override void OnEnabled()
    {
        _noteCollector.OnTriggetEnter += OnCanPress;
        _noteCollector.OnTriggerExit += OnTirggerExit;
        _deadZone.OnDeadZone += Fail;
        _button.onClick.AddListener(OnCollect);
    }
    protected override void OnDisabled()
    {
        _noteCollector.OnTriggetEnter -= OnCanPress;
        _noteCollector.OnTriggerExit -= OnTirggerExit;
        _deadZone.OnDeadZone -= Fail;
        _button.onClick.RemoveListener(OnCollect);
    }
    private void OnCollect()
    {
        if (_canBePressed)
        {
            OnCollected?.Invoke();
            _note.ResetNote();
            VisualEffects.Instance.PlayEffect(_effect);
            AudioMusic.Instance.PlayClip(_hitClip);
        }
        else
        {
            AudioMusic.Instance.PlayClip(_missClip);
            Fail();
        }
    }
    private void OnCanPress(Note note)
    {
        if (note.GetColor() == _color)
        {
            _canBePressed = true;
            _note = note;
        }
    }
    private void OnTirggerExit()
    {
        _canBePressed = false;
        _note = null;
    }
    private void Fail()
    {
        OnWrong?.Invoke();
    }
    protected override void Run()
    {
        if (Input.GetKeyDown(_keyCode))
        {
            OnCollect();
        }
    }
}
