using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : ObjectPool
{
    public event UnityAction OnFinish;
    public event UnityAction<string> OnFinishSave;
    public static event UnityAction OnFirstNote;
    public static event UnityAction OnLastNote;

    [SerializeField] private NoteMover _template;
    [SerializeField] private float _timeBeforeEnd;
    [SerializeField] private Transform _spawnPosition;
    [SerializeField] private Transform _finishPosition;
    [SerializeField] private Transform _removePosition;

    private Song _song;
    private float _beatsShownInAdvance;

    private int _nextIndex = 0;
    private bool _isPlaying;

    private void Start()
    {
        _song = AudioFlow.Instance.GetSong();
        _beatsShownInAdvance = _song.BeatsShownInAdvance;
        Initialize(_template);
    }

    protected override void Run()
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
                    note.SetIsRecord(false);
                    note.SetPositions(_spawnPosition, _finishPosition);// это комментить
                    note.SetBeatOfThisNote(_song.Notes[_nextIndex]);// это комментить
                    note.SetSpawner(this);
                    _nextIndex++;

                    if (_nextIndex == 1)
                    {
                        OnFirstNote?.Invoke();
                    }
                }
            }
        }
        else
        {
            StartCoroutine(OnLastNotePlayed());
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
        OnFinishSave?.Invoke(_song.SongName);
    }

    private IEnumerator OnLastNotePlayed()
    {
        yield return new WaitForSeconds(2.5f);
        OnLastNote?.Invoke();
    }

}
