using NTC.Global.Cache;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class SongRecorder : MonoCache
{
    public static SongRecorder Instance { get; private set; }


    [SerializeField] private Song _song;
    [SerializeField] private TMP_Text _recordText;
    [SerializeField] private bool _devRecording;

    private List<float> _newNote = new List<float>();


    private bool _isPlaying = false;

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
        //AudioFlow.Instance.SetSong(_song);
    }/*
    protected override void Run()
    {
        if (!_isPlaying)
            return;

        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            _newNote.Add(AudioFlow.Instance.GetSongPosInBeats());
            _recordText.text = ("Note: " + AudioFlow.Instance.GetSongPosInBeats().ToString());
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            _song.SetNotes(_newNote.ToArray());
            _recordText.text = ("Saved: " + _song.Notes.Length);

            Debug.Log("Saved");
        }
    }*/

    public void SetSong(Song song)
    {
        if (!_devRecording)
        {
            _song = song;
            AudioFlow.Instance.SetSong(_song);

        }
    }

    public void StartSong()
    {
        AudioFlow.Instance.StartFlow();
    }
    public void StopSong()
    {
        AudioFlow.Instance.PauseFlow();
    }

    public void RecordNote()
    {
        _newNote.Add(AudioFlow.Instance.GetSongPosInBeats());
        _recordText.text = ("Note: " + AudioFlow.Instance.GetSongPosInBeats().ToString());
    }

    public void SaveRecord()
    {
        _song.SetNotes(_newNote.ToArray());
        _recordText.text = ("Saved: " + _song.Notes.Length);

        Debug.Log("Saved");
    }
}
