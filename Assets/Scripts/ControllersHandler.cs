using NTC.Global.Cache;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ControllersHandler : MonoCache
{
    public static event UnityAction OnAnyCollected;
    public static event UnityAction OnAnyMissed;

    public static ControllersHandler Instance;  

    [SerializeField] private CollectorController[] _collectorControllers;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There more than one ControllersHandler!" + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    protected override void OnEnabled() 
    {
        for (int i = 0; i < _collectorControllers.Length; i++)
        {
            _collectorControllers[i].OnCollected += OnControllerCollected;
            _collectorControllers[i].OnWrong += OnControllerMissed;
        }
    }
    protected override void OnDisabled() 
    {
        for (int i = 0; i < _collectorControllers.Length; i++)
        {
            _collectorControllers[i].OnCollected -= OnControllerCollected;
            _collectorControllers[i].OnWrong -= OnControllerMissed;
        }
    }

    private void OnControllerCollected() 
    {
        OnAnyCollected?.Invoke();
    }

    private void OnControllerMissed() 
    {
        OnAnyMissed?.Invoke();
    }

}
