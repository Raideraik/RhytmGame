using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioEffectsControll : MonoBehaviour
{
    public static AudioEffectsControll Instance { get; private set; }

    [SerializeField] private AudioSource _effectSource;
    [SerializeField] private AudioClip _missClip, _hitClip, _buttonClip;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There more than one AudioMusic!" + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void PlayHitClip()
    {
        _effectSource.PlayOneShot(_hitClip);
    }
    public void PlayMissClip()
    {
        _effectSource.PlayOneShot(_missClip);
    }
    public void PlayButtonClip()
    {
        _effectSource.PlayOneShot(_buttonClip);
    }
}
