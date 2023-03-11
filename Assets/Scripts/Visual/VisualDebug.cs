using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VisualDebug : MonoBehaviour
{
    private TMP_Text _text;

    [SerializeField] private bool _onPower;
    [SerializeField] private bool _onColor;
    [SerializeField] private bool _onEnded;

    private void OnEnable()
    {
        if (_onPower)
            CharacterVisual._onPower += OnPower;
        if (_onColor)
            CharacterVisual._onColor += OnColor;
        if (_onEnded)
            CharacterVisual._onEnded += OnEnded;
    }

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    private void OnPower() 
    {
        _text.text = "Power: " + "True";
    }
    
    private void OnColor() 
    {
        _text.text = "Color: " + "True";
    }
    
    private void OnEnded() 
    {
        _text.text = "Ended: " + "True";
    }

}
