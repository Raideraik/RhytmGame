using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuMusic : MonoBehaviour
{
    [SerializeField] private Song[] _songs;
    [SerializeField] private AudioSource _musicSource;

    private void Start()
    {
        _musicSource.clip = _songs[Random.Range(0, _songs.Length)].Clip;
        _musicSource.Play();
    }
}
