using NTC.Global.Cache;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class SongRecorder : MonoCache
{
    [SerializeField] private Song _song;
    [SerializeField] private TMP_Text _recordText;
    private List<float> _newNote = new List<float>();

    private void Start()
    {
        AudioFlow.Instance.SetSong(_song);
    }
    protected override void Run()
    {
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
    }
}
