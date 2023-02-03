using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SongRecorder : MonoBehaviour
{
    [SerializeField] private Song _song;
    private List<float> _newNote = new List<float>();

    private void Start()
    {
        AudioFlow.Instance.SetSong(_song);

    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            _newNote.Add(AudioFlow.Instance.GetSongPosInBeats());
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            _song.SetNotes(_newNote.ToArray());
            Debug.Log("Saved");
        }

    }
}
