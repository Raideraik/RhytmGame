using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : ObjectPool
{
    public event UnityAction OnFinish;

    [SerializeField] private NoteMover _template;
    [SerializeField] private bool _createMode;

    private Song _song;
    private int _beatsShownInAdvance;

    private int _nextIndex = 0;
    private bool _isPlaying;
    /*
    private void Start()
    {
        _secPerBeat = 60f / _song.Bpm;
        _dsptimesong = (float)AudioSettings.dspTime;
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _song.Clip;
    }*/

    private void Start()
    {
        _song = AudioFlow.Instance.GetSong();
        _beatsShownInAdvance = _song.BeatsShownInAdvance;
        Initialize(_template);
    }

    private void Update()
    {
        if (!_isPlaying)
            return;

        if (!_createMode)
        {
            if (_nextIndex < _song.Notes.Length)
            {
                if (_song.Notes[_nextIndex] < AudioFlow.Instance.GetSongPosInBeats() + _beatsShownInAdvance)
                {
                    if (TryGetObject(out NoteMover note))
                    {
                        note.gameObject.SetActive(true);
                        note.UpdateCollor();
                        note.SetBeatOfThisNote(_song.Notes[_nextIndex]);
                        note.SetSpawner(this);
                        _nextIndex++;
                    }
                }
            }
            else
            {
                OnFinish?.Invoke();
            }
        }
    }
    public float GetBeatsShownInAdvance()
    {
        return _beatsShownInAdvance;
    }


    public void StartGame()
    {
        _isPlaying = true;
    }

}
