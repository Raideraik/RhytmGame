using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    public event UnityAction OnFinish;

    [SerializeField] private Song _song;
    [SerializeField] private float _beatsShownInAdvance;
    [SerializeField] private NoteMover _template;
    [SerializeField] private bool _createMode;
    [SerializeField] private Transform _container;

    private List<float> _newNote = new List<float>();

    private float _songPosition;
    private float _songPosInBeats;
    private float _secPerBeat;
    private float _dsptimesong;
    private int _nextIndex = 0;
    private AudioSource _audioSource;
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
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!_isPlaying)
            return;

        _songPosition = (float)(AudioSettings.dspTime - _dsptimesong);
        _songPosInBeats = _songPosition / _secPerBeat;

        if (_createMode)
        {
            if (Input.GetKeyDown(KeyCode.Keypad0))
            {
                _newNote.Add(_songPosInBeats);
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                _song.SetNotes(_newNote.ToArray());
                Debug.Log("Saved");
            }
        }
        else
        {
            if (_nextIndex < _song.Notes.Length)
            {
                if (_song.Notes[_nextIndex] < _songPosInBeats + _beatsShownInAdvance)
                {

                    NoteMover note = Instantiate(_template, _container);

                    note.SetBeatOfThisNote(_song.Notes[_nextIndex]);
                    note.SetSpawner(this);

                    _nextIndex++;
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
    public float GetSongPosInBeats()
    {
        return _songPosInBeats;
    }

    public void StartGame()
    {
        _isPlaying = true;

        _secPerBeat = 60f / _song.Bpm;
        _dsptimesong = (float)AudioSettings.dspTime;
        _audioSource.clip = _song.Clip;
        _audioSource.Play();
    }
}
