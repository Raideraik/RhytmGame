using NTC.Global.Cache;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SongRecorder : MonoCache
{
    public static SongRecorder Instance { get; private set; }

    [SerializeField] private Song _song;
    [SerializeField] private bool _devRecording;
    [SerializeField] private SongChooseToRecordUI _recordUI;

    private List<float> _newNote = new List<float>();

    public bool DevRecording => _devRecording;
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There more than one SongRecorder!" + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        if (_devRecording)
        {
            AudioFlow.Instance.SetSong(_song);
        }
    }

    protected override void Run()
    {
        if (_devRecording)
        {
            if (Input.GetKeyDown(KeyCode.Keypad0))
            {
                RecordNote();
                Debug.Log(AudioFlow.Instance.GetSongPosInBeats());

            }
        }
    }

    protected override void OnEnabled()
    {
        _recordUI.OnPlayButton += PlaySong;
        _recordUI.OnSaveButton += SaveRecord;
        _recordUI.OnRecordButton += RecordNote;
    }

    protected override void OnDisabled()
    {
        _recordUI.OnPlayButton -= PlaySong;
        _recordUI.OnSaveButton -= SaveRecord;
        _recordUI.OnRecordButton -= RecordNote;
    }

    public void SetSong(Song song)
    {
        if (!_devRecording)
        {
            _song = song;
            AudioFlow.Instance.SetSong(_song);
        }
    }

    private void PlaySong()
    {
        AudioFlow.Instance.StartFlow();
        _newNote.Clear();
    }

    private void RecordNote()
    {
        _newNote.Add(AudioFlow.Instance.GetSongPosInBeats());
        //_recordText.text = ("Note: " + AudioFlow.Instance.GetSongPosInBeats().ToString());
    }

    private void SaveRecord()
    {
        _song.SetNotes(_newNote.ToArray());
        //_recordText.text = ("Saved: " + _song.Notes.Length);

        Debug.Log("Saved");
    }
}