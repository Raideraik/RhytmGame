using NTC.Global.Cache;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteVisualEffects : MonoCache
{
    public static NoteVisualEffects Instance { get; private set; }

    [SerializeField] private ParticleSystem[] _hitEffect;
    //[SerializeField] private CollectorController[] _collectorControllers;
    //[SerializeField] private ControllersHandler _controllersHandler;
    [SerializeField] private NoteCollector _noteCollector;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There more than one VisualEffects!" + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    protected override void OnEnabled()
    {
        ControllersHandler.OnAnyCollected += PlayHitEffect;
    }

    protected override void OnDisabled()
    {
        ControllersHandler.OnAnyCollected -= PlayHitEffect;
    }

    public void PlayHitEffect()
    {
        for (int i = 0; i < _hitEffect.Length; i++)
        {
            _hitEffect[i].Play();
        }
    }
}
