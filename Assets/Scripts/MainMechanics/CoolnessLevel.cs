using NTC.Global.Cache;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoolnessLevel : MonoCache
{
    public event UnityAction OnLoseGame;
    public event UnityAction<float> OnCurrentCoolnessLevelChange;

    [SerializeField] private int _maxLevel = 10;
    [SerializeField] private int _minLevel = 0;
    [SerializeField] private DeadZone _deadZone;

    private int _currentLevel;
    private int _accumulative = 1;

    public int MaxLevel => _maxLevel;
    public int MinLevel => _minLevel;
    protected override void OnEnabled()
    {
        ControllersHandler.OnAnyCollected += AddCoolness;
        _deadZone.OnDeadZone += RemoveCoolness;
    }
    protected override void OnDisabled()
    {
        ControllersHandler.OnAnyCollected += AddCoolness;
        _deadZone.OnDeadZone -= RemoveCoolness;
    }

    private void Start()
    {
        _currentLevel = _maxLevel / 2;
    }

    private void AddCoolness()
    {
        if (_currentLevel < _maxLevel)
        {
            _currentLevel++;
            _accumulative = 1;
            OnCurrentCoolnessLevelChange?.Invoke(_currentLevel);
        }
    }
    private void RemoveCoolness()
    {
        if (_currentLevel >= _minLevel)
        {
            _currentLevel -= _accumulative;
            _accumulative++;
            OnCurrentCoolnessLevelChange?.Invoke(_currentLevel);
        }
        else
        {
            OnLoseGame?.Invoke();
        }
    }
}
