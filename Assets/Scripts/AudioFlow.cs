using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class AudioFlow : MonoBehaviour
{
    public static AudioFlow Instance { get; private set; }

    private float _songPosition;
    private float _songPosInBeats;
    private float _secPerBeat;
    private float _dsptimesong;
    private AudioSource _audioSource;

    private Song _song;
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There more than one AudioFlow!" + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;

        _audioSource = GetComponent<AudioSource>();

    }


    private void Update()
    {
        _songPosition = (float)(AudioSettings.dspTime - _dsptimesong);
        _songPosInBeats = _songPosition / _secPerBeat;
    }

    public float GetSongPosInBeats()
    {
        return _songPosInBeats;
    }

    public Song GetSong() 
    {
        return _song;
    }

    public void SetSong(Song song)
    {
        _song = song;
    }

    public void StartFlow()
    {
        _secPerBeat = 60f / _song.Bpm;
        _dsptimesong = (float)AudioSettings.dspTime;

        _audioSource.clip = _song.Clip;
        _audioSource.Play();
    }


}
