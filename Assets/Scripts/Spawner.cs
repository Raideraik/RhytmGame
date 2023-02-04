using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : ObjectPool
{
    public event UnityAction OnFinish;

    [SerializeField] private NoteMover _template;
    [SerializeField] private float _timeBeforeEnd;
    [SerializeField] private Transform _spawnPosition;
    [SerializeField] private Transform _removePosition;

    private Song _song;
    private float _beatsShownInAdvance;

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

        if (_nextIndex < _song.Notes.Length)
        {
            if (_song.Notes[_nextIndex] < AudioFlow.Instance.GetSongPosInBeats() + _beatsShownInAdvance)
            {
                if (TryGetObject(out NoteMover note))
                {
                    note.gameObject.SetActive(true);
                    note.SetPositions(_spawnPosition, _removePosition);
                  //  note.UpdateCollor();
                    note.SetBeatOfThisNote(_song.Notes[_nextIndex]);
                    note.SetSpawner(this);
                    _nextIndex++;
                }
            }
        }
        else
        {
            StartCoroutine(OnSongFinish());
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

    private IEnumerator OnSongFinish()
    {
        yield return new WaitForSeconds(_timeBeforeEnd);
        OnFinish?.Invoke();
    }

}
