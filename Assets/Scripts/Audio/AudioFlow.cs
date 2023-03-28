using NTC.Global.Cache;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public class AudioFlow : MonoCache
{
    public event UnityAction OnSongSeted;
    public static AudioFlow Instance { get; private set; }

    private float _songPosition;
    private float _songPosInBeats;
    private float _secPerBeat;
    private float _dsptimesong;
    private AudioSource _audioSource;

    private Song _song;
    private bool _isPlaying = true;
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
    protected override void Run()
    {
        if (!_isPlaying)
            return;

        _songPosition = (float)(AudioSettings.dspTime - _dsptimesong);
       // _songPosInBeats = _songPosition / _secPerBeat;
    }
    protected override void FixedRun()
    {
        if (!_isPlaying)
            return;

       // _songPosition = (float)(AudioSettings.dspTime - _dsptimesong) ;
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
        OnSongSeted?.Invoke();
    }
    public void StartFlow()
    {
        if (!_song.IsPrivate)
        {
            _audioSource.clip = _song.Clip;
        }

        _secPerBeat = 60f / _song.Bpm;
        _dsptimesong = (float)AudioSettings.dspTime;

        _audioSource.Play();

    }

    public void PauseFlow()
    {
        _isPlaying = false;
        _audioSource.Pause();
    }

    public void ContinueFlow()
    {
        _isPlaying = true;
        _audioSource.Play();
        _dsptimesong = (float)AudioSettings.dspTime - _songPosition;
    }
}
