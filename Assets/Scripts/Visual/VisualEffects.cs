using NTC.Global.Cache;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualEffects : MonoCache
{
    public static VisualEffects Instance { get; private set; }
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

    public void PlayEffect(ParticleSystem effect)
    {
        effect.Play();
    }
}
