using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettingsMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Slider _music;
    [SerializeField] private Slider _effects;

    private void Start()
    {
        // _audioMixer.SetFloat("Music", PlayerPrefs.GetFloat("Music"));
        _music.value = PlayerPrefs.GetFloat("Music");
        // _audioMixer.SetFloat("Music", PlayerPrefs.GetFloat("Music"));
        _effects.value = PlayerPrefs.GetFloat("Effects");

    }


    public void SetMusic(float volume)
    {
        _audioMixer.SetFloat("Music", volume);
        PlayerPrefs.SetFloat("Music", volume);
    }
    public void SetEffects(float volume)
    {
        _audioMixer.SetFloat("Effects", volume);
        PlayerPrefs.SetFloat("Effects", volume);
    }

}
