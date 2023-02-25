using NTC.Global.Cache;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSong : MonoCache
{
    [SerializeField] private Song[] _songs;
    private void Awake()
    {
        for (int i = 0; i < _songs.Length; i++)
        {
            if (_songs[i].Id == PlayerPrefs.GetInt("ChoosedSong", 0))
            {
                AudioFlow.Instance.SetSong(_songs[i]);
            }
        }
    }
}
