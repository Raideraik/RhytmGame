using NTC.Global.Cache;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteVisualEffects : MonoCache
{

    public static NoteVisualEffects Instance { get; private set; }

    [SerializeField] private List<ParticleSystem> _multiplayerEffect;
    [SerializeField] private ParticleSystem _hitEffect;
    //[SerializeField] private CollectorController[] _collectorControllers;
    //[SerializeField] private ControllersHandler _controllersHandler;
    [SerializeField] private Score _score;

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
        ControllersHandler.OnAnyCollected += HitEffect;
        _score.OnMultiplierChanged += MultiplierAdded;
    }

    protected override void OnDisabled()
    {
        _score.OnMultiplierChanged -= MultiplierAdded;
        ControllersHandler.OnAnyCollected -= HitEffect;
    }

    public void MultiplierAdded()
    {
        for (int i = 0; i < _multiplayerEffect.Count; i++)
        {
            _multiplayerEffect[i].Play();
        }
    }

    private void HitEffect()
    {
        _hitEffect.Play();
    }

    public void AddEffect(ParticleSystem effect)
    {
        _multiplayerEffect.Add(effect);
    }
}
