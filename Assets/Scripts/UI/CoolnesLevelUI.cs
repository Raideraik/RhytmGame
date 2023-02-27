using NTC.Global.Cache;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(CoolnessLevel))]
public class CoolnesLevelUI : MonoCache
{
    [SerializeField] private Slider _coolnessMetr;
    [SerializeField] private float _fillSpeed = 10f;

    private CoolnessLevel _coolnessLevel;
    private bool _isChanging = false;
    private float _target;

    private void Awake()
    {
        _coolnessLevel = GetComponent<CoolnessLevel>();
    }

    private void Start()
    {
        _coolnessMetr.maxValue = _coolnessLevel.MaxLevel;
        _coolnessMetr.minValue = _coolnessLevel.MinLevel;
        _coolnessMetr.value = _coolnessLevel.MaxLevel / 2;
    }

    protected override void OnEnabled()
    {
        _coolnessLevel.OnCurrentCoolnessLevelChange += ChangeCoolness;
    }

    protected override void OnDisabled()
    {
        _coolnessLevel.OnCurrentCoolnessLevelChange -= ChangeCoolness;
    }

    private void ChangeCoolness(float currentLevel)
    {
        _target = currentLevel;
        _isChanging = true;
    }

    protected override void Run()
    {
        if (_isChanging)
        {
            _coolnessMetr.value = Mathf.MoveTowards(_coolnessMetr.value, _target, _fillSpeed * Time.deltaTime);
            if (_coolnessMetr.value == _target)
                _isChanging = false;
        }

    }

}
