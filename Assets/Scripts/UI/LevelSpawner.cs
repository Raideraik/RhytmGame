using NTC.Global.Cache;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoCache
{

    [SerializeField] private Song[] _songs;
    [SerializeField] private GameObject _container;
    [SerializeField] private ChooseSong _template;

    private void Start()
    {
        for (int i = 0; i < _songs.Length; i++)
        {
            ChooseSong song = Instantiate(_template, _container.transform);
            song.SetSong(_songs[i]);
            song.SetStars();
        }
    }
}
