using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CollectorController : MonoBehaviour
{
    public event UnityAction OnCollected;
    public event UnityAction OnWrong;

    [SerializeField] private NoteCollector _noteCollector;
    [SerializeField] private DeadZone _deadZone;
    [SerializeField] private Button _button;
    [SerializeField] private Collor _color;
    [SerializeField] private KeyCode _keyCode;
    private bool _canBePressed;
    private Note _note;
    //private float _score;

    private void OnEnable()
    {
        _noteCollector.OnTriggetEnter += OnCanPress;
        _noteCollector.OnTriggerExit += OnTirggerExit;
        _deadZone.OnDeadZone += Fail;
        _button.onClick.AddListener(OnCollect);
    }
    private void OnDisable()
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
            _note.gameObject.SetActive(false);
        }
        else
        {
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

    private void Update()
    {
        if (Input.GetKeyDown(_keyCode))
        {
            OnCollect();
            //_score++;
            //Debug.Log(_score);
        }
    }
}
