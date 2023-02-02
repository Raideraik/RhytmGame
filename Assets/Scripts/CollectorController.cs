using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectorController : MonoBehaviour
{
    [SerializeField] private NoteCollector _noteCollector;
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
        _button.onClick.AddListener(OnCollect);
    }
    private void OnDisable()
    {
        _noteCollector.OnTriggetEnter -= OnCanPress;
        _noteCollector.OnTriggerExit -= OnTirggerExit;
        _button.onClick.RemoveListener(OnCollect);
    }
    private void OnCollect()
    {
        if (_canBePressed)
        {
            _note.gameObject.SetActive(false);
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

    private void Update()
    {
        if (Input.GetKeyDown(_keyCode) && _canBePressed)
        {
            OnCollect();
            //_score++;
            //Debug.Log(_score);
        }
    }
}
