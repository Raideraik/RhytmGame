using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSong : MonoBehaviour
{
    [SerializeField] private Song[] _songs;
    [SerializeField] private Spawner _spawner;
    private void Start()
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
